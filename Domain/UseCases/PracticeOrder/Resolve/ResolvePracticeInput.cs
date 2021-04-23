using Domain.Abstractions.Mediatr;
using FluentValidation;

namespace Domain.UseCases.PracticeOrder.Resolve
{
    public class ResolvePracticeInput: IUseCaseInput
    {
        public int PracticeId { get; set; }
        public int TeacherId { get; set; }
    }
    
    public class ResolvePracticeInputValidator: AbstractValidator<ResolvePracticeInput>
    {
        public ResolvePracticeInputValidator()
        {
            RuleFor(x => x.PracticeId).NotEmpty();
            RuleFor(x => x.TeacherId).NotEmpty();
        }
    }
}