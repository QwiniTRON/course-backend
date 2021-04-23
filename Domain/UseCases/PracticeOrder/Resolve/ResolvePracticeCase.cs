using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Data;

namespace Domain.UseCases.PracticeOrder.Resolve
{
    public class ResolvePracticeCase: IUseCase<ResolvePracticeInput>
    {
        private readonly IAppDbContext _context;

        public ResolvePracticeCase(IAppDbContext context)
        {
            _context = context;
        }

        
        public async Task<IOutput> Handle(ResolvePracticeInput request, CancellationToken cancellationToken)
        {
            var practice = await _context.PracticeOrders.FindAsync(request.PracticeId);

            if (practice is null)
            {
                return ActionOutput.Error("Заявка на прохождение не нашлась");
            }
            
            if (practice.IsDone)
            {
                return ActionOutput.Error("Решение уже принято");
            }

            practice.IsDone = true;
            practice.IsResolved = true;
            practice.TeacherId = request.TeacherId;

            _context.PracticeOrders.Update(practice);

            await _context.SaveChangesAsync(cancellationToken);
            
            return ActionOutput.Success;
        }
    }
}