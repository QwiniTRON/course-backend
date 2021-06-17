using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Abstractions.Services;
using Domain.Abstractions.Services.IMailClient;
using Domain.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Domain.UseCases.Account.SignIn
{
    public class SignInCase: IUseCase<SignInInput>
    {
        private readonly IAppDbContext _context;
        private readonly ILogger<SignInCase> _logger;
        private readonly SignInManager<Entity.User> _signInManager;
        private readonly IAuthDataProvider _dataProvider;

        public SignInCase(IAppDbContext context, ILogger<SignInCase> logger, SignInManager<Entity.User> signInManager, IAuthDataProvider dataProvider)
        {
            _context = context;
            _logger = logger;
            _signInManager = signInManager;
            _dataProvider = dataProvider;
        }
        

        public async Task<IOutput> Handle(SignInInput request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(x => x.RolesEntities)
                .ThenInclude(x => x.Role)
                .FirstOrDefaultAsync(x => x.Mail == request.Mail, cancellationToken);

            if (user is null)
            {
                _logger.LogInformation("User {name} was not found", request.Mail);
                return ActionOutput.Error("Пользователь не найден");
            }

            if (user.IsBanned == true)
            {
                _logger.LogInformation("User {name} tried to enter with ban", request.Mail);
                return ActionOutput.Error("Пользователь забанен.");
            }
            
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (signInResult.Succeeded == false)
            {
                _logger.LogInformation("User {name} was not found", request.Mail);
                return ActionOutput.Error("Данные не верны"); 
            }
            
            _logger.LogInformation($"User {user} signed in");
            
            var identity = _dataProvider.GetIdentity(request.Mail);

            if (identity is null)
            {
                return ActionOutput.Error("Пользователь не найден");
            }

            return ActionOutput.SuccessData(new {token = _dataProvider.GetJwtByIdentity(identity)});
        }
    }
}