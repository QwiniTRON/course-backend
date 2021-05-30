using Domain.Abstractions.Mediatr;
using FluentValidation;

namespace Domain.UseCases.PracticeOrder.CurrentUserPractices
{
    public class CurrentUserPracticesInput: IUseCaseInput
    {
        public int UserId { get; set; }
    }
    
    public class CurrentUserPracticesInputValidator: AbstractValidator<CurrentUserPracticesInput>
    {
        public CurrentUserPracticesInputValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("user id обязательно");
        }
    }
}