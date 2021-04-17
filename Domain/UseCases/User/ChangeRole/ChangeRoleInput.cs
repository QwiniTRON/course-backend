using Domain.Abstractions.Mediatr;
using Domain.Enums;
using Domain.UseCases.User.Bann;
using FluentValidation;

namespace Domain.UseCases.User.ChangeRole
{
    public class ChangeRoleInput: IUseCaseInput
    {
        public int UserId { get; set; }
        public UserRoles NewRole { get; set; }
    }
    
    public class ChangeRoleInputValidator : AbstractValidator<ChangeRoleInput>
    {
        public ChangeRoleInputValidator()
        {
            RuleFor(x => x.NewRole).NotEmpty().WithMessage("Роль обязательна");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Пользователь обязателен");
        }
    }
}