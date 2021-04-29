using Domain.Abstractions.Mediatr;
using FluentValidation;

namespace Domain.UseCases.User.ChangePassword
{
    public class ChangePasswordInput: IUseCaseInput
    {
        public string UserId { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
    }
    
    public class ChangePasswordInputValidator: AbstractValidator<ChangePasswordInput>
    {
        public ChangePasswordInputValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.OldPassword).NotEmpty();
            RuleFor(x => x.NewPassword).NotNull().WithMessage("Пароль обязателен")
                .MaximumLength(64).WithMessage("Пароль не длинее 64 символов")
                .MinimumLength(6).WithMessage("Пароль не короче 6 символов");
        }
    }
}