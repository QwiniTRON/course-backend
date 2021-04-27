using Domain.Abstractions.Mediatr;
using FluentValidation;

namespace Domain.UseCases.Subject.GetOne
{
    public class GetSubjectInfoInput: IUseCaseInput
    {
        public int? SubjectId { get; set; }   
        public string SubjectName { get; set; }   
    }
    
    public class GetSubjectInfoInputValidator: AbstractValidator<GetSubjectInfoInput>
    {
        public GetSubjectInfoInputValidator()
        {
            RuleFor(x => x.SubjectName)
                .MinimumLength(1).When(request => request.SubjectId == null);
        }
    }
}