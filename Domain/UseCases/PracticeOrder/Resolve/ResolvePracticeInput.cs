using Domain.Abstractions.Mediatr;
using FluentValidation;

namespace Domain.UseCases.PracticeOrder.Resolve
{
    public class ResolvePracticeInput: IUseCaseInput
    {
        
    }
    
    public class ResolvePracticeInputValidator: AbstractValidator<ResolvePracticeInput>
    {
        public ResolvePracticeInputValidator()
        {
            
        }
    }
}