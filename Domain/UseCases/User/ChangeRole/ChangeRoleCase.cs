using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Abstractions.Services;
using Domain.Data;
using Domain.Enums;
using Domain.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.User.ChangeRole
{
    public class ChangeRoleCase: IUseCase<ChangeRoleInput>
    {
        private readonly IAppDbContext _context;
        private readonly UserManager<Entity.User> _userManager;
        private readonly RoleManager<Entity.User> _roleManager;
        private readonly ICurrentUserProvider _currentUserProvider;

        public ChangeRoleCase(IAppDbContext context, UserManager<Entity.User> userManager, ICurrentUserProvider currentUserProvider)
        {
            _context = context;
            _userManager = userManager;
            _currentUserProvider = currentUserProvider;
        }

        
        public async Task<IOutput> Handle(ChangeRoleInput request, CancellationToken cancellationToken)
        {
            var currentUesrId = (await _currentUserProvider.GetCurrentUser()).Id;
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            
            if (user is null)
            {
                return ActionOutput.Error("Пользователя не существует.");
            }
            
            if (user.Id == currentUesrId)
            {
                return ActionOutput.Error("Нельзя менять себе роль");
            }
            
            var newRole = request.NewRole;

            var userRoles = await _userManager.GetRolesAsync(user);
            var addResult = await _userManager.AddToRoleAsync(user, Enum.GetName(newRole));
            var removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles);

            return ActionOutput.Success;
        }
    }
}