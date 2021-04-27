using Domain.Abstractions.Mediatr;
using FluentValidation;

namespace Domain.UseCases.Progress.ProgressesById
{
    public class ProgressesByIdInput: IUseCaseInput
    {
        public int UserId { get; private set; }
        public int SubjectId { get; set; }
        
        public int SetUserId(int id) => UserId = id;
    }
    
    public class ProgressesByIdInputValidator: AbstractValidator<ProgressesByIdInput>
    {
        public ProgressesByIdInputValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.SubjectId).NotEmpty();
        }
    }
}