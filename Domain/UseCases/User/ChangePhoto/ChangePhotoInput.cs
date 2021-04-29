using Domain.Abstractions.Mediatr;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Domain.UseCases.User.ChangePhoto
{
    public class ChangePhotoInput: IUseCaseInput
    {
        public int UserId { get; set; }
        public IFormFile NewPhoto { get; set; }
    }
    
    public class ChangePhotoInputValidator: AbstractValidator<ChangePhotoInput>
    {
        public ChangePhotoInputValidator()
        {
            RuleFor(x => x.NewPhoto).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
        }        
    }
}