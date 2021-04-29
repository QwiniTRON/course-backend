using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Microsoft.AspNetCore.Identity;

namespace Domain.UseCases.User.ChangePassword
{
    public class ChangePasswordCase: IUseCase<ChangePasswordInput>
    {
        private readonly UserManager<Entity.User> _userManager;

        public ChangePasswordCase(UserManager<Entity.User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IOutput> Handle(ChangePasswordInput request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user is null)
            {
                return ActionOutput.Error("Пользователь не найден");
            }

            var changeResult = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

            if (changeResult.Succeeded == false)
            {
                return ActionOutput.Error(changeResult.Errors.FirstOrDefault().ToString());
            }
            
            return ActionOutput.Success;
        }
    }
}