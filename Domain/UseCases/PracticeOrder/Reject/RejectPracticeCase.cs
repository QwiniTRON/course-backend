using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Data;

namespace Domain.UseCases.PracticeOrder.Reject
{
    public class RejectPracticeCase: IUseCase<RejectPracticeInput>
    {
        private readonly IAppDbContext _context;

        public RejectPracticeCase(IAppDbContext context)
        {
            _context = context;
        }
        

        public async Task<IOutput> Handle(RejectPracticeInput request, CancellationToken cancellationToken)
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
            practice.RejectReason = request.Description;
            practice.IsResolved = false;

            _context.PracticeOrders.Update(practice);

            await _context.SaveChangesAsync(cancellationToken);
            
            return ActionOutput.Success;
        }
    }
}