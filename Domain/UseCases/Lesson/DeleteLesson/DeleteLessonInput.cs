using Domain.Abstractions.Mediatr;
using FluentValidation;

namespace Domain.UseCases.Lesson.DeleteLesson
{
    public class DeleteLessonInput: IUseCaseInput
    {
        public int LessonId { get; set; }
    }
    
    public class DeleteLessonInputValidator: AbstractValidator<DeleteLessonInput>
    {
        public DeleteLessonInputValidator()
        {
            RuleFor(x => x.LessonId).NotEmpty();
        }
    }
}