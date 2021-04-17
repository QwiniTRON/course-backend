using Domain.Abstractions.Mediatr;
using FluentValidation;

namespace Domain.UseCases.User.Test
{
    public class TestInput: IUseCaseInput
    {
        public string SomeText { get; set; }
    }

    public class TestInputValidator : AbstractValidator<TestInput>
    {
        public TestInputValidator()
        {
            RuleFor(x => x.SomeText).NotEmpty();
        }
    }
}