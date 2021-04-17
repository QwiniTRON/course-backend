using Domain.Abstractions.Mediatr;
using FluentValidation;

namespace Domain.UseCases.Account.SignIn
{
    public class SignInInput: IUseCaseInput
    {
        public string Mail { get; set; }
        public string Password { get; set; }
    }

    public class SignInInputValidator : AbstractValidator<SignInInput>
    {
        public SignInInputValidator()
        {
            
        }
    }
}