using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Data;
using Domain.Maps.Views;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.Lesson.EditLesson
{
    public class EditLessonCase: IUseCase<EditLessonInput>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public EditLessonCase(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IOutput> Handle(EditLessonInput request, CancellationToken cancellationToken)
        {
            var lesson = await _context.Lessons.FirstOrDefaultAsync(x => x.Id == request.LessonId);

            lesson.Name = request.Name;
            lesson.Content = request.Content;
            lesson.Description = request.Description;
            lesson.Index = request.Index;
            lesson.IsPractice = request.IsPractice;

            await _context.SaveChangesAsync(cancellationToken);
            
            return ActionOutput.SuccessData(_mapper.Map<LessonView>(lesson));
        }
    }
}