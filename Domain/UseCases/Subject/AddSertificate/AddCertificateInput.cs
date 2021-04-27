using Domain.Abstractions.Mediatr;
using FluentValidation;

namespace Domain.UseCases.Subject.AddSertificate
{
    public class AddCertificateInput: IUseCaseInput
    {
        public int UserId { get; set; }
        public int SubjectId { get; set; }
    }
    
    public class AddCertificateInputValidator: AbstractValidator<AddCertificateInput>
    {
        public AddCertificateInputValidator()
        {
            RuleFor(x => x.SubjectId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}