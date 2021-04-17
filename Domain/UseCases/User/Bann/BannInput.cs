using Domain.Abstractions.Mediatr;
using FluentValidation;

namespace Domain.UseCases.User.Bann
{
    public class BannInput: IUseCaseInput
    {
        public int UserForBannId { get; set; }
    }

    public class BannInputValidator : AbstractValidator<BannInput>
    {
        public BannInputValidator()
        {
            RuleFor(x => x.UserForBannId).NotEmpty();
        }
    }
}