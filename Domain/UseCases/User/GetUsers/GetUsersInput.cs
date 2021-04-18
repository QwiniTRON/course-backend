using Domain.Abstractions.Mediatr;
using FluentValidation;

namespace Domain.UseCases.User.GetUsers
{
    public class GetUsersInput: IUseCaseInput
    {
        public string Search { get; set; }
        public int? Page { get; set; }
        public int? Limit { get; set; }
    }

    public class GetUsersInputValidator: AbstractValidator<GetUsersInput>
    {
        public GetUsersInputValidator()
        {
            RuleFor(x => x.Search).NotEmpty();
        }
    }
}