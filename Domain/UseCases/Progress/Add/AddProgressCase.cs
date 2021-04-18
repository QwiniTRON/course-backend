using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Data;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.Progress.Add
{
    public class AddProgressCase: IUseCase<AddProgressInput>
    {
        private IAppDbContext _context;

        public AddProgressCase(IAppDbContext context)
        {
            _context = context;
        }
        

        public async Task<IOutput> Handle(AddProgressInput request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken: cancellationToken);

            if (user is null)
            {
                return ActionOutput.Error("User wasn't found");
            }

            var lesson = await _context.Lessons.FindAsync(request.LessonId);

            if (lesson is null)
            {
                return ActionOutput.Error("Lesson wasn't found");
            }
            
            var progress = new UserProgress(user, lesson);

            await _context.Progresses.AddAsync(progress, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            
            return ActionOutput.SuccessData(new {progressId = progress.Id});
        }
    }
}