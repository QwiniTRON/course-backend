using Domain.Abstractions.Mediatr;
using FluentValidation;

namespace Domain.UseCases.User.ChangeNick
{
    public class ChangeNickInput: IUseCaseInput
    {
        public string NewNick { get; set; }

        public string CurrentUserMail { get; set; }
    }
    
    public class ChangeNickInputValidator: AbstractValidator<ChangeNickInput>
    {
        public ChangeNickInputValidator()
        {
            RuleFor(x => x.NewNick)
                .NotNull().WithMessage("Ник обязятелен")
                .Length(6, 64).WithMessage("Ник не короче 6 и не длиннее 64");
        }
    }
}