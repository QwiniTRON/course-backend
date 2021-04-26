using Domain.Abstractions.Mediatr;
using FluentValidation;

namespace Domain.UseCases.Comment.GetByLesson
{
    public class CommentByLessonInput: IUseCaseInput
    {
        public int LessonId { get; set; }
    }
    
    public class CommentsByLessonInputValidator: AbstractValidator<CommentByLessonInput>
    {
        public CommentsByLessonInputValidator()
        {
            RuleFor(x => x.LessonId).NotEmpty();
        }
    }
}