using Domain.Abstractions.Mediatr;
using FluentValidation;

namespace Domain.UseCases.PracticeOrder.Reject
{
    public class RejectPracticeInput: IUseCaseInput
    {
        public int PracticeId { get; set; }
        public int TeacherId { get; set; }
        public string Description { get; set; } = "";
    }
    
    public class RejectPracticeInputValidator: AbstractValidator<RejectPracticeInput>
    {
        public RejectPracticeInputValidator()
        {
            RuleFor(x => x.PracticeId).NotEmpty();
            RuleFor(x => x.TeacherId).NotEmpty();
        }
    }
}