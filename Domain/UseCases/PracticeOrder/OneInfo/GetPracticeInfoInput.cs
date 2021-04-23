using Domain.Abstractions.Mediatr;
using FluentValidation;

namespace Domain.UseCases.PracticeOrder.OneInfo
{
    public class GetPracticeInfoInput: IUseCaseInput
    {
        public int PracticeId { get; set; }
    }
    
    public class GetPracticeInfoInputValidator: AbstractValidator<GetPracticeInfoInput>
    {
        public GetPracticeInfoInputValidator()
        {
            RuleFor(x => x.PracticeId).NotEmpty();
        }
    }
}