using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Abstractions.Services;
using Domain.Data;
using Domain.UseCases.Account.SignIn;
using Microsoft.Extensions.Logging;

namespace Domain.UseCases.Account.Check
{
    public class CheckCase: IUseCase<CheckInput>
    {
        private readonly IAppDbContext _context;
        private readonly ILogger<SignInCase> _logger;
        private readonly IAuthDataProvider _dataProvider;
        private readonly ICurrentUserProvider _currentUserProvider;

        public CheckCase(IAppDbContext context, ILogger<SignInCase> logger, IAuthDataProvider dataProvider, ICurrentUserProvider currentUserProvider)
        {
            _context = context;
            _logger = logger;
            _dataProvider = dataProvider;
            _currentUserProvider = currentUserProvider;
        }


        public async Task<IOutput> Handle(CheckInput request, CancellationToken cancellationToken)
        {
            var user = await _currentUserProvider.GetCurrentUser();

            if (user is null)
            {
                return ActionOutput.Error("Вы не авторизованы");
            }

            var identity = _dataProvider.GetIdentity(user.Mail);

            if (identity is null)
            {
                return ActionOutput.Error("Пользователь не найден");
            }
            
            return ActionOutput.SuccessData(new {token = _dataProvider.GetJwtByIdentity(identity)});
        }
    }
}