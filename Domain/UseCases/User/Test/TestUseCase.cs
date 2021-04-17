using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Data;

namespace Domain.UseCases.User.Test
{
    public class TestUseCase: IUseCase<TestInput>
    {
        private IAppDbContext _context;

        public TestUseCase(IAppDbContext context)
        {
            _context = context;
        }

        
        public async Task<IOutput> Handle(TestInput request, CancellationToken cancellationToken)
        {
            return ActionOutput.SuccessData(request.SomeText);
        }
    }
}