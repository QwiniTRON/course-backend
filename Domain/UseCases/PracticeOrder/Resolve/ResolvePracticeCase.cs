using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;

namespace Domain.UseCases.PracticeOrder.Resolve
{
    public class ResolvePracticeCase: IUseCase<ResolvePracticeInput>
    {
        public Task<IOutput> Handle(ResolvePracticeInput request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}