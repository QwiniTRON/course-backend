using Domain.Abstractions.Mediatr;
using Domain.Enums;
using FluentValidation;

namespace Domain.UseCases.PracticeOrder.GetOne
{
    public class GetOnePracticeInput: IUseCaseInput
    {
        public int UserId { get; set; }
        public bool Last { get; set; }
    }
    
    public class GetOnePracticeInputValidator: AbstractValidator<GetOnePracticeInput>
    {
        public GetOnePracticeInputValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }   
    }
}