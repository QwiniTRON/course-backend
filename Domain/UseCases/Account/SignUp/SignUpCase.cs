using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Abstractions.Services;
using Domain.Data;
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

        public SignUpCase(IAppDbContext context, ILogger<SignUpCase> logger, UserManager<Entity.User> userManager, IAuthDataProvider dataProvider)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _dataProvider = dataProvider;
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
            
            await _userManager.AddToRoleAsync(user, UserRoles.Participant.ToString());
            await _userManager.AddPasswordAsync(user, request.Password);

            var userWithRoles = await _context.Users
                .Include(u => u.RolesEntities).ThenInclude(re => re.Role)
                .FirstAsync(x => x.Id == user.Id, cancellationToken);

            _logger.LogInformation($"User {user} was registered");
            
            var identity = _dataProvider.GetIdentity(request.Mail);

            if (identity is null)
            {
               return ActionOutput.Error("Данные не верны");
            }

            return ActionOutput.SuccessData(new {token = _dataProvider.GetJwtByIdentity(identity)});
        }
    }
}