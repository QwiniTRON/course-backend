using Domain.Abstractions.Mediatr;
using FluentValidation;

namespace Domain.UseCases.Comment.Delete
{
    public class DeleteCommentInput: IUseCaseInput
    {
        public int CommentId { get; set; }
    }
    
    public class DeleteCommentInputValidator: AbstractValidator<DeleteCommentInput>
    {
        public DeleteCommentInputValidator()
        {
            RuleFor(x => x.CommentId).NotEmpty();
        }
    }
}