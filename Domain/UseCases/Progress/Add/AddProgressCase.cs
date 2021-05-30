using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Data;
using Domain.Entity;
using Domain.Views.UserProgress;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.Progress.Add
{
    public class AddProgressCase: IUseCase<AddProgressInput>
    {
        private IAppDbContext _context;
        private IMapper _mapper;

        public AddProgressCase(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        

        public async Task<IOutput> Handle(AddProgressInput request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken: cancellationToken);

            if (user is null)
            {
                return ActionOutput.Error("Пользователь не был найден");
            }

            var lesson = await _context.Lessons.FindAsync(request.LessonId);

            if (lesson is null)
            {
                return ActionOutput.Error("Урок не был найден");
            }

            if (lesson.IsPractice)
            {
                return ActionOutput.Error("Урок практический");
            }

            var hasEqualProgress = await _context.Progresses
                .AnyAsync(x => x.UserId == user.Id && x.LessonId == lesson.Id, 
                    cancellationToken: cancellationToken);

            if (hasEqualProgress)
            {
                return ActionOutput.Error("Данный урок уже засчитан");
            }
            
            var progress = new UserProgress(user, lesson);

            await _context.Progresses.AddAsync(progress, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            var newUserProgresses =
                await _context.Progresses.Where(x => x.UserId == user.Id).ToListAsync(cancellationToken);
            
            return ActionOutput.SuccessData(_mapper.Map<List<UserProgressView>>(newUserProgresses));
        }
    }
}