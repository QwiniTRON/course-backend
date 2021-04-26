using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Data;

namespace Domain.UseCases.Comment.Edit
{
    public class EditCommentCase: IUseCase<EditCommentInput>
    {
        private readonly IAppDbContext _context;

        public EditCommentCase(IAppDbContext context)
        {
            _context = context;
        }

        
        public async Task<IOutput> Handle(EditCommentInput request, CancellationToken cancellationToken)
        {
            var comment = await _context.Comments.FindAsync(request.CommentId);

            if (comment is null)
            {
                return ActionOutput.Error("Комментария не существует");
            }

            comment.Text = request.NewText;

            await _context.SaveChangesAsync(cancellationToken);

            return ActionOutput.Success;
        }
    }
}