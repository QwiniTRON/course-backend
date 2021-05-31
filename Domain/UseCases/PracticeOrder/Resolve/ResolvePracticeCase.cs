using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Abstractions.Services.IMailClient;
using Domain.Abstractions.Services.ITemplateCreator;
using Domain.Data;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

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
            var practice = await _context.PracticeOrders
                .Include(x => x.Author)
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
            practice.IsResolved = true;
            practice.TeacherId = request.TeacherId;

            _context.PracticeOrders.Update(practice);

            await _context.SaveChangesAsync(cancellationToken);
            
            var progress = new UserProgress(practice.Author, practice.Lesson);
            await _context.Progresses.AddAsync(progress, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _mailClient.SendMail(practice.Author.Mail, message =>
            {
                message.Subject = $"Практический урок {practice.Lesson.Name} пройден.";
                message.Body = _templateCreator.GetResolveLetter(practice.Lesson.Name);
            });
            
            return ActionOutput.Success;
        }
    }
}