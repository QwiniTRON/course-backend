using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.Lesson.DeleteLesson
{
    public class DeleteLessonCase: IUseCase<DeleteLessonInput>
    {
        private readonly IAppDbContext _context;

        public DeleteLessonCase(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<IOutput> Handle(DeleteLessonInput request, CancellationToken cancellationToken)
        {
            var lesson = await _context.Lessons.FirstOrDefaultAsync(x => x.Id == request.LessonId);

            _context.Lessons.Remove(lesson);

            await _context.SaveChangesAsync(cancellationToken);
            
            return ActionOutput.Success;
        }
    }
}