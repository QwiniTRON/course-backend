using Domain.Abstractions.Mediatr;
using Domain.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Domain.UseCases.Account.SignUp
{
    public class SignUpInput: IUseCaseInput
    {
        public string Mail { get; set; }
        public string Nick { get; set; }
        public string Password { get; set; }
        
        public IFormFile UserPhoto { get; set; }
    }

    public class SignUpInputValidator : AbstractValidator<SignUpInput>
    {
        public SignUpInputValidator()
        {
            RuleFor(x => x.Mail).NotNull().WithMessage("Почта обязательна").EmailAddress().WithMessage("Почта не по формату");

            RuleFor(x => x.Nick).NotNull().WithMessage("Ник обязятелен").Length(6, 64).WithMessage("Ник не короче 6 и не длиннее 64");

            RuleFor(x => x.Password).NotNull().WithMessage("Пароль обязателен")
                .MaximumLength(64).WithMessage("Пароль не длинее 64 символов")
                .MinimumLength(6).WithMessage("Пароль не короче 6 символов");
        }
    }
}