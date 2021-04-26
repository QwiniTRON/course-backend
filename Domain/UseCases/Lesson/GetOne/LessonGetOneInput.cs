using Domain.Abstractions.Mediatr;
using FluentValidation;

namespace Domain.UseCases.Lesson.GetOne
{
    public class LessonGetOneInput: IUseCaseInput
    {
        public int LessonId { get; set; }
    }
    
    public class LessonGetOneInputValidator: AbstractValidator<LessonGetOneInput>
    {
        public LessonGetOneInputValidator()
        {
            RuleFor(x => x.LessonId).NotEmpty();
        }
    }
}