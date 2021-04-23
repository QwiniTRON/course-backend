using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Abstractions.Services;
using Domain.Data;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.PracticeOrder.Add
{
    public class AddPracticeOrderCase: IUseCase<AddPracticeOrderInput>
    {
        private readonly IAppDbContext _context;
        private readonly IFileUploader _fileUploader;

        public AddPracticeOrderCase(IAppDbContext context, IFileUploader fileUploader)
        {
            _context = context;
            _fileUploader = fileUploader;
        }
        

        public async Task<IOutput> Handle(AddPracticeOrderInput request, CancellationToken cancellationToken)
        {
            var author = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken: cancellationToken);

            if (author is null)
            {
                return ActionOutput.Error("Пользователь не был найден");
            }
            
            var lesson = await _context.Lessons
                .FirstOrDefaultAsync(x => x.Id == request.LessonId, cancellationToken: cancellationToken);
            
            if (lesson is null)
            {
                return ActionOutput.Error("Урок не был найден");
            }

            using var unit = _context.CreateUnitOfWork();

            var fileSaveResult = await _fileUploader.SaveFile(request.CodeFile);
            var filePath = fileSaveResult.Data.OperatedFilePath;
            var fileEntity = new AppFile(request.CodeFile.FileName, filePath);
            await _context.AppFiles.AddAsync(fileEntity, cancellationToken);

            var order = new Entity.PracticeOrder(author, lesson, fileEntity);
            await _context.PracticeOrders.AddAsync(order, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            await unit.Apply();
            
            return ActionOutput.SuccessData(order.Id);
        }
    }
}