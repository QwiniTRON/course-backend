using Domain.Abstractions.Mediatr;
using FluentValidation;

namespace Domain.UseCases.Lesson
{
    public class GetLessonsInput: IUseCaseInput
    {
        
    }
    
    public class GetLessonsInputValidator: AbstractValidator<GetLessonsInput>
    {
        public GetLessonsInputValidator()
        {
            
        }
    }
}