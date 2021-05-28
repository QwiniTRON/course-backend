using Domain.Abstractions.Mediatr;
using FluentValidation;

namespace Domain.UseCases.Lesson.AddLesson
{
    public class AddLessonInput: IUseCaseInput
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public bool IsPractice { get; set; }
        
        // public int SubjectId { get; set; }
    }
    
    public class AddLessonInputValidator: AbstractValidator<AddLessonInput>
    {
        public AddLessonInputValidator()
        {
            RuleFor(x => x.Content).NotNull().WithMessage("содержание обязательно");
            RuleFor(x => x.Description).NotNull().WithMessage("описание обязательно");
            RuleFor(x => x.Index).NotNull().WithMessage("индекс обязателен");
            RuleFor(x => x.Name).NotNull().WithMessage("имя обязательно");
            RuleFor(x => x.IsPractice).NotNull().WithMessage("статус обязателен");
        }
    }
}