using Domain.Abstractions.Mediatr;
using FluentValidation;

namespace Domain.UseCases.User.UserInfo
{
    public class UserInfoInput: IUseCaseInput
    {
        public int UserId { get; set; } 
    }
    
    public class UserInfoInputValidator: AbstractValidator<UserInfoInput>
    {
        public UserInfoInputValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}