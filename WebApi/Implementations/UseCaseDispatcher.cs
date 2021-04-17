using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using course_backend.Presentation;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace course_backend.Implementations
{
    public interface IUseCaseDispatcher
    {
        public Task<IActionResult> DispatchAsync<TRequest>(TRequest request)
            where TRequest: IUseCaseInput;
    }
    
    public class UseCaseDispatcher: IUseCaseDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMediator _mediator;


        public UseCaseDispatcher(IServiceProvider serviceProvider, IMediator mediator)
        {
            _serviceProvider = serviceProvider;
            _mediator = mediator;
        }

        public async Task<IActionResult> DispatchAsync<TRequest>(TRequest request) where TRequest : IUseCaseInput
        {
            using var services = _serviceProvider.CreateScope();

            var validator = services.ServiceProvider.GetService<IValidator<TRequest>>();

            if (validator is null)
            {
                throw new Exception("Validator wasn't found");
            }

            var validationResult = await validator.ValidateAsync(request, CancellationToken.None);

            if (!validationResult.IsValid)
            {
                var actionPresenter = services.ServiceProvider.GetService<IPresenter<ActionOutput>>();

                return actionPresenter.Present(ActionOutput.Error(validationResult.Errors.First().ErrorMessage));
            }

            var result = await _mediator.Send(request);

            var presenterType = typeof(IPresenter<>).MakeGenericType(result.GetType());
            var presenter = services.ServiceProvider.GetService(presenterType);

            if (presenter is null)
            {
                throw new Exception("Presenter for this output was not found " + result.GetType().Name);
            }

            var method = 
                presenter.GetType().GetMethod(nameof(IPresenter<IOutput>.Present), new [] {result.GetType()});
            
            return (IActionResult) method!.Invoke(presenter, new object[] {result});
        }
    }
}