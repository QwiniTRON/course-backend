using Domain.Abstractions.Outputs;
using MediatR;

namespace Domain.Abstractions.Mediatr
{
    public interface IUseCaseInput: IRequest<IOutput>
    {
        
    }
}