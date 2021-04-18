using Domain.Abstractions.Mediatr;
using FluentValidation;

namespace Domain.UseCases.Progress.Add
{
    public class AddProgressInput: IUseCaseInput
    {
        public int UserId { get; set; }
        public int LessonId { get; set; }
    }
    
    public class AddProgressInputValidator: AbstractValidator<AddProgressInput>
    {
        public AddProgressInputValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.LessonId).NotEmpty();
        }
    } 
}