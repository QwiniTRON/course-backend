using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Abstractions.Services;
using Domain.Data;
using Domain.Entity;
using Domain.Services;
using Domain.UseCases.PracticeOrder.GetOne;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.PracticeOrder.Add
{
    public class AddPracticeOrderCase: IUseCase<AddPracticeOrderInput>
    {
        private readonly IAppDbContext _context;
        private readonly IFileUploader _fileUploader;
        private readonly IPracticeOrderProvider _practiceOrderProvider;

        public AddPracticeOrderCase(IAppDbContext context, 
            IFileUploader fileUploader, 
            IPracticeOrderProvider practiceOrderProvider)
        {
            _context = context;
            _fileUploader = fileUploader;
            _practiceOrderProvider = practiceOrderProvider;
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

            if (lesson.IsPractice == false)
            {
                return ActionOutput.Error("Урок не практический");
            }

            /* check last order */
            var lastOrder = await _practiceOrderProvider.GetUserPracticeOrders(author.Id, lesson.Id);
            if (lastOrder.FirstOrDefault()?.IsDone == false)
            {
                return ActionOutput.Error("Заявка уже оформлена");
            }
            
            
            using var unit = _context.CreateUnitOfWork();

            var fileSaveResult = await _fileUploader.SaveFile(request.CodeFile);

            if (fileSaveResult.Succeeded == false)
            {
                return ActionOutput.Error("Что-то пошло не так");
            }
            
            var filePath = fileSaveResult.Data.OperatedFilePath;
            var filePathRelated = fileSaveResult.Data.OperatedFileRelatedPath;
            var fileEntity = new AppFile(request.CodeFile.FileName, filePath, filePathRelated);
            await _context.AppFiles.AddAsync(fileEntity, cancellationToken);

            var order = new Entity.PracticeOrder(author, lesson, fileEntity);
            await _context.PracticeOrders.AddAsync(order, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            await unit.Apply();
            
            return ActionOutput.SuccessData(order.Id);
        }
    }
}