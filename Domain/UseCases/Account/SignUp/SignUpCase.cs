using System.Threading;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Abstractions.Services;
using Domain.Data;
using Domain.Entity;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Domain.UseCases.Account.SignUp
{
    public class SignUpCase: IUseCase<SignUpInput>
    {
        private readonly IAppDbContext _context;
        private readonly ILogger<SignUpCase> _logger;
        private readonly UserManager<Entity.User> _userManager;
        private readonly IAuthDataProvider _dataProvider;
        private readonly IFileUploader _fileUploader;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        
        public SignUpCase(IAppDbContext context, ILogger<SignUpCase> logger, UserManager<Entity.User> userManager, IAuthDataProvider dataProvider, IFileUploader fileUploader,Microsoft.Extensions.Configuration.IConfiguration configuration1)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _dataProvider = dataProvider;
            _fileUploader = fileUploader;
            _configuration = configuration1;
        }

        public async Task<IOutput> Handle(SignUpInput request, CancellationToken cancellationToken)
        {
            var hasSameNick = await _context.Users
                .AnyAsync(x => x.Nick == request.Nick, cancellationToken: cancellationToken);
            if (hasSameNick)
            {
                return ActionOutput.Error("Пользователь с таким ником уже зарегистрирован");
            }
            
            var user = new Entity.User(request.Mail) { Nick = request.Nick};
            var registerResult = await _userManager.CreateAsync(user);
            if (registerResult.Succeeded == false)
            {
                return ActionOutput.Error("Такой пользователь уже есть");
            }
            
            using var unit = _context.CreateUnitOfWork();

            var userPhotoPath = _configuration.GetSection("Static:DefaultUserPhoto").Value;
            if (request.UserPhoto != null)
            {
                var fileSaveResult = await _fileUploader.SaveFile(request.UserPhoto);
                if (fileSaveResult.Succeeded == false)
                {
                    return ActionOutput.Error("Что-то пошло не так");
                }
                var filePath = fileSaveResult.Data.OperatedFilePath;
                var filePathRelated = fileSaveResult.Data.OperatedFileRelatedPath;
                var fileEntity = new AppFile(request.UserPhoto.FileName, filePath, filePathRelated);
                userPhotoPath = filePathRelated;
                await _context.AppFiles.AddAsync(fileEntity, cancellationToken);
                user.UserPhoto = fileEntity;
            }

            user.Photo = userPhotoPath;
            
            await _userManager.AddToRoleAsync(user, UserRoles.Participant.ToString());
            await _userManager.AddPasswordAsync(user, request.Password);

            _logger.LogInformation($"User {user} was registered");
            
            await _context.SaveChangesAsync(cancellationToken);
            await unit.Apply();
            
            var identity = _dataProvider.GetIdentity(request.Mail);
            if (identity is null)
            {
                return ActionOutput.Error("Данные не верны");
            }
            
            return ActionOutput.SuccessData(new {token = _dataProvider.GetJwtByIdentity(identity)});
        }
    }
}