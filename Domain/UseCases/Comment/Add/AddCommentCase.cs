using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Abstractions.Services;
using Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.Comment.Add
{
    public class AddCommentCase: IUseCase<AddCommentInput>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserProvider _currentUserProvider;

        public AddCommentCase(IAppDbContext context, ICurrentUserProvider currentUserProvider)
        {
            _context = context;
            _currentUserProvider = currentUserProvider;
        }

        public async Task<IOutput> Handle(AddCommentInput request, CancellationToken cancellationToken)
        {
            var currentUser = await _currentUserProvider.GetCurrentUser();

            if (currentUser is null)
            {
                return ActionOutput.Error("Пользователь не найден");
            }

            var lesson = await _context.Lessons.FirstOrDefaultAsync(
                x => x.Id == request.LessonId,
                cancellationToken: cancellationToken);
            
            if (lesson is null)
            {
                return ActionOutput.Error("Урок не найден");
            }
            
            var comment = new Entity.Comment(request.Text, currentUser, lesson);

            await _context.Comments.AddAsync(comment, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            
            return ActionOutput.SuccessData(comment.Id);
        }
    }
}