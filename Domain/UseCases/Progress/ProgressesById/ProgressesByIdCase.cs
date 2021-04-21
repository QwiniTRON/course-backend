using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.Progress.ProgressesById
{
    public class ProgressesByIdCase: IUseCase<ProgressesByIdInput>
    {
        private readonly IAppDbContext _context;

        public ProgressesByIdCase(IAppDbContext context)
        {
            _context = context;
        }
        

        public async Task<IOutput> Handle(ProgressesByIdInput request, CancellationToken cancellationToken)
        {
            var progresses = await _context.Progresses
                .Where(x => x.UserId == request.UserId)
                .ToListAsync(cancellationToken: cancellationToken);

            return ActionOutput.SuccessData(progresses);
        }
    }
}