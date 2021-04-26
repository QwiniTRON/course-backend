using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.Comment.GetByLesson
{
    public class CommentsByLessonCase: IUseCase<CommentByLessonInput>
    {
        private readonly IAppDbContext _context;

        public CommentsByLessonCase(IAppDbContext context)
        {
            _context = context;
        }
        

        public async Task<IOutput> Handle(CommentByLessonInput request, CancellationToken cancellationToken)
        {
            var comments = await _context.Comments.AsNoTracking()
                .Include(x => x.Lesson)
                .Where(x => x.Lesson.Id == request.LessonId)
                .ToListAsync(cancellationToken: cancellationToken);
            
            return ActionOutput.SuccessData(comments);
        }
    }
}