using Domain.Abstractions.Mediatr;
using FluentValidation;

namespace Domain.UseCases.Progress.ProgressesById
{
    public class ProgressesByIdInput: IUseCaseInput
    {
        public int UserId { get; set; }
    }
    
    public class ProgressesByIdInputValidator: AbstractValidator<ProgressesByIdInput>
    {
        public ProgressesByIdInputValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}