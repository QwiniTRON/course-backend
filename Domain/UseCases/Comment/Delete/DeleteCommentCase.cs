using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.Comment.Delete
{
    public class DeleteCommentCase: IUseCase<DeleteCommentInput>
    {
        private readonly IAppDbContext _context;

        public DeleteCommentCase(IAppDbContext context)
        {
            _context = context;
        }
        

        public async Task<IOutput> Handle(DeleteCommentInput request, CancellationToken cancellationToken)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(
                x => x.Id == request.CommentId, 
                cancellationToken: cancellationToken);

            if (comment is null)
            {
                return ActionOutput.Error("Комментария не существует.");
            }

            _context.Comments.Remove(comment);

            var affectedRecords = await _context.SaveChangesAsync(cancellationToken);
            
            return ActionOutput.SuccessData(affectedRecords);
        }
    }
}