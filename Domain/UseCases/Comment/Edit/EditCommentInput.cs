using Domain.Abstractions.Mediatr;
using FluentValidation;

namespace Domain.UseCases.Comment.Edit
{
    public class EditCommentInput: IUseCaseInput
    {
        public int CommentId { get; set; }
        public string NewText { get; set; }
    }
    
    public class EditCommentInputValidator: AbstractValidator<EditCommentInput>
    {
        public EditCommentInputValidator()
        {
            RuleFor(x => x.CommentId).NotEmpty();
            RuleFor(x => x.NewText).NotEmpty().Length(3, 512);
        }
    }
}