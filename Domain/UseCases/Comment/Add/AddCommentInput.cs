using Domain.Abstractions.Mediatr;
using FluentValidation;

namespace Domain.UseCases.Comment.Add
{
    public class AddCommentInput: IUseCaseInput
    {
        public int LessonId { get; set; }
        public string Text { get; set; }
    }
    
    public class AddCommentInputValidator: AbstractValidator<AddCommentInput>
    {
        public AddCommentInputValidator()
        {
            RuleFor(x => x.LessonId).NotEmpty();
            RuleFor(x => x.Text).NotEmpty().Length(3, 512);
        }
    }
}