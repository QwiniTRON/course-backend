using Domain.Abstractions.Mediatr;
using FluentValidation;

namespace Domain.UseCases.Lesson.EditLesson
{
    public class EditLessonInput: IUseCaseInput
    {
        public int LessonId { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public bool IsPractice { get; set; }
    }
    
    public class EditLessonInputValidator: AbstractValidator<EditLessonInput>
    {
        public EditLessonInputValidator()
        {
            RuleFor(x => x.LessonId).NotNull().WithMessage("id обязателен");
            RuleFor(x => x.Content).NotNull().WithMessage("содержание обязательно");
            RuleFor(x => x.Description).NotNull().WithMessage("описание обязательно");
            RuleFor(x => x.Index).NotNull().WithMessage("индекс обязателен");
            RuleFor(x => x.Name).NotNull().WithMessage("имя обязательно");
            RuleFor(x => x.IsPractice).NotNull().WithMessage("статус обязателен");
        }
    }
}