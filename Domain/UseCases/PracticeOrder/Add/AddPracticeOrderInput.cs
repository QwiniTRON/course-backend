using Domain.Abstractions.Mediatr;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Domain.UseCases.PracticeOrder.Add
{
    public class AddPracticeOrderInput: IUseCaseInput
    {
        public int UserId { get; set; }   
        
        public int LessonId { get; set; }

        public IFormFile CodeFile { get; set; }
    }

    public class AddPracticeOrderInputValidator: AbstractValidator<AddPracticeOrderInput>
    {
        public AddPracticeOrderInputValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.LessonId).NotEmpty();

            RuleFor(x => x.CodeFile).NotEmpty();
        }
    }
}