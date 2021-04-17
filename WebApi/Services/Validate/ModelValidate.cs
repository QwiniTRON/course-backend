using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using course_backend.Presentation;
using Domain.Abstractions.Outputs;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace course_backend.Services
{
    public interface IModelValidate
    {
        Task<ValidateResult> Validate<TRequest>(TRequest request, IServiceProvider service);
    }

    public class ModelValidate : IModelValidate
    {
        public ModelValidate()
        {
        }

        public async Task<ValidateResult> Validate<TRequest>(TRequest request, IServiceProvider services)
        {
            var validator = services.GetService<IValidator<TRequest>>();

            if (validator is null)
            {
                throw new Exception("Validator wasn't found");
            }

            var validationResult = await validator.ValidateAsync(request, CancellationToken.None);

            if (!validationResult.IsValid)
            {
                return ValidateResult.Error(validationResult.Errors.First().ErrorMessage.ToString());
            }
            
            return ValidateResult.Success();
        }
    }
}