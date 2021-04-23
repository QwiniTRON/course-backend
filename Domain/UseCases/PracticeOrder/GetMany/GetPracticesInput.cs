using Domain.Abstractions.Mediatr;
using FluentValidation;

namespace Domain.UseCases.PracticeOrder.GetMany
{
    public class GetPracticesInput: IUseCaseInput
    {
        public string Search { get; set; }
        public int? Page { get; set; }
        public int? Limit { get; set; }
    }
    
    public class GetPracticesInputValidator: AbstractValidator<GetPracticesInput>
    {
        public GetPracticesInputValidator()
        {
            
        }
    }
}