using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Abstractions.Services.IMailClient;
using Domain.Abstractions.Services.ITemplateCreator;
using Domain.Data;

namespace Domain.UseCases.PracticeOrder.Resolve
{
    public class ResolvePracticeCase: IUseCase<ResolvePracticeInput>
    {
        private readonly IAppDbContext _context;
        private readonly IMailClient _mailClient;
        private readonly ITemplateCreator _templateCreator;

        public ResolvePracticeCase(IAppDbContext context, IMailClient mailClient, ITemplateCreator templateCreator)
        {
            _context = context;
            _mailClient = mailClient;
            _templateCreator = templateCreator;
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
            
            _mailClient.SendMail("barabanzz871@gmail.com", message =>
            {
                message.Subject = $"Практический урок {practice.Lesson.Name} пройден.";
                message.Body = _templateCreator.GetResolveLetter(practice.Lesson.Name);
            });
            
            return ActionOutput.Success;
        }
    }
}