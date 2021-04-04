using Domain.Abstractions.Outputs;
using MediatR;

namespace Domain.Abstractions.Mediatr
{
    public interface IUseCase<in TRequest>: IRequestHandler<TRequest, IOutput> where TRequest: IUseCaseInput
    {
        
    }
}