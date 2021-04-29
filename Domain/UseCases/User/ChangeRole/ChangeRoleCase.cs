using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
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

        public ChangeRoleCase(IAppDbContext context, UserManager<Entity.User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        
        public async Task<IOutput> Handle(ChangeRoleInput request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            var newRole = request.NewRole;

            if (user is null)
            {
                return ActionOutput.Error("Пользователя не существует.");
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var addResult = await _userManager.AddToRoleAsync(user, Enum.GetName(newRole));
            var removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles);

            return ActionOutput.Success;
        }
    }
}