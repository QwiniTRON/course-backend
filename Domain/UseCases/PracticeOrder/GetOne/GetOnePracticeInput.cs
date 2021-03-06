using System.Text.Json.Serialization;
using Domain.Abstractions.Mediatr;
using FluentValidation;

namespace Domain.UseCases.PracticeOrder.GetOne
{
    public class GetOnePracticeInput: IUseCaseInput
    {
        public int UserId { get; set; }
        public int LessonId { get; set; }
        public bool Last { get; set; }
    }
    
    public class GetOnePracticeInputValidator: AbstractValidator<GetOnePracticeInput>
    {
        public GetOnePracticeInputValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.LessonId).NotEmpty();
        }   
    }
}