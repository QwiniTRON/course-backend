using Domain.Abstractions.Mediatr;
using FluentValidation;

namespace Domain.UseCases.Account.Check
{
    public class CheckInput: IUseCaseInput
    {
    }
    
    public class CheckInputValidator: AbstractValidator<CheckInput>
    {
        public CheckInputValidator()
        {
        }
    }
}