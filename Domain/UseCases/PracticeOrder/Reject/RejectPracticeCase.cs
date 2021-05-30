using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Abstractions.Services.IMailClient;
using Domain.Abstractions.Services.ITemplateCreator;
using Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.PracticeOrder.Reject
{
    public class RejectPracticeCase: IUseCase<RejectPracticeInput>
    {
        private readonly IAppDbContext _context;
        private readonly IMailClient _mailClient;
        private readonly ITemplateCreator _templateCreator;

        public RejectPracticeCase(IAppDbContext context, IMailClient mailClient, ITemplateCreator templateCreator)
        {
            _context = context;
            _mailClient = mailClient;
            _templateCreator = templateCreator;
        }
        

        public async Task<IOutput> Handle(RejectPracticeInput request, CancellationToken cancellationToken)
        {
            var practice = await _context.PracticeOrders
                .Include(x => x.Lesson)
                .FirstOrDefaultAsync(x => x.Id == request.PracticeId, cancellationToken: cancellationToken);

            if (practice is null)
            {
                return ActionOutput.Error("Заявка на прохождение не нашлась");
            }
            
            if (practice.IsDone)
            {
                return ActionOutput.Error("Решение уже принято");
            }

            if (practice.AuthorId == request.TeacherId)
            {
                return ActionOutput.Error("Нельзя принять практику у себя");
            }

            practice.IsDone = true;
            practice.RejectReason = request.Description;
            practice.IsResolved = false;
            practice.TeacherId = request.TeacherId;

            _context.PracticeOrders.Update(practice);

            await _context.SaveChangesAsync(cancellationToken);
            
            _mailClient.SendMail("barabanzz871@gmail.com", message =>
            {
                message.Subject = $"Практический урок {practice.Lesson.Name} не пройден.";
                message.Body = _templateCreator.GetRejectLetter(practice.Lesson.Name, request.Description);
            });
            return ActionOutput.Success;
        }
    }
}