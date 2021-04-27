using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Data;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.Subject.AddSertificate
{
    public class AddCertificateCase: IUseCase<AddCertificateInput>
    {
        private readonly IAppDbContext _context;

        public AddCertificateCase(IAppDbContext context)
        {
            _context = context;
        }

        
        public async Task<IOutput> Handle(AddCertificateInput request, CancellationToken cancellationToken)
        {
            var subject = await _context.Subjects
                .Include(x => x.Lessons)
                .FirstOrDefaultAsync(x => x.Id == request.SubjectId, 
                    cancellationToken: cancellationToken);
            if (subject is null)
            {
                return ActionOutput.Error("Данного предмета не существует");
            }

            var user = await _context.Users
                .Include(x => x.UserProgresses)
                    .ThenInclude(x => x.Lesson)
                .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken: cancellationToken);
            if (user is null)
            {
                return ActionOutput.Error("Пользователь не найден");
            }

            /* has such certificate */
            var lastSertificate = await _context.SubjectSertificates
                .FirstOrDefaultAsync(
                    x => x.OwnerId == request.UserId && x.SubjectId == request.SubjectId, 
                    cancellationToken: cancellationToken);
            if (lastSertificate != null)
            {
                return ActionOutput.Error("Данный сертификат уже получен");
            }

            /* count progresses to certificate */
            var doneLessonCount = user.UserProgresses.Count(x => x.Lesson.SubjectId == subject.Id);
            var lessonForSertificate = subject.Lessons.Count;
            if (doneLessonCount != lessonForSertificate)
            {
                return ActionOutput.Error("Пройдены не все уроки");
            }
            
            var certificate = new SubjectSertificate(user, subject);
            _context.SubjectSertificates.Add(certificate);
            await _context.SaveChangesAsync(cancellationToken);
            
            return ActionOutput.SuccessData(certificate.Id);
        }
    }
}