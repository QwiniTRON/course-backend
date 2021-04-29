using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Abstractions.Services;
using Domain.Data;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.User.ChangePhoto
{
    public class ChangePhotoCase: IUseCase<ChangePhotoInput>
    {
        private readonly IFileUploader _fileUploader;
        private readonly IAppDbContext _context;

        public ChangePhotoCase(IFileUploader fileUploader, IAppDbContext context)
        {
            _fileUploader = fileUploader;
            _context = context;
        }

        
        public async Task<IOutput> Handle(ChangePhotoInput request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken: cancellationToken);

            if (user is null)
            {
                return ActionOutput.Error("Пользователь не найден");
            }
            
            using var unit = _context.CreateUnitOfWork();

            try
            {
                var oldFile = await _context.AppFiles
                    .FirstOrDefaultAsync(x => x.Id == user.UserPhotoId, cancellationToken: cancellationToken);

                user.UserPhoto = null;

                var deleteResult = await _fileUploader.DeleteFile(oldFile.Path);
            }
            catch (Exception e) {}

            var fileSaveResult = await _fileUploader.SaveFile(request.NewPhoto);
            if (fileSaveResult.Succeeded == false)
            {
                return ActionOutput.Error("Что-то пошло не так");
            }
            var filePath = fileSaveResult.Data.OperatedFilePath;
            var filePathRelated = fileSaveResult.Data.OperatedFileRelatedPath;
            var fileEntity = new AppFile(request.NewPhoto.FileName, filePath, filePathRelated);
            await _context.AppFiles.AddAsync(fileEntity, cancellationToken);
            user.UserPhoto = fileEntity;
            user.Photo = filePathRelated;
            
            await _context.SaveChangesAsync(cancellationToken);
            await unit.Apply();
            
            return ActionOutput.Success;
        }
    }
}