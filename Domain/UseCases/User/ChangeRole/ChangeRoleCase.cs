using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Data;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.User.ChangeRole
{
    public class ChangeRoleCase: IUseCase<ChangeRoleInput>
    {
        private readonly IAppDbContext _context;
        private readonly UserManager<Entity.User> _userManager;

        public ChangeRoleCase(IAppDbContext context, UserManager<Entity.User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        
        public async Task<IOutput> Handle(ChangeRoleInput request, CancellationToken cancellationToken)
        {
            Entity.User user = await _userManager.FindByIdAsync(request.UserId.ToString());
            var role = request.NewRole;

            if (user is null)
            {
                return ActionOutput.Error("Пользователя не существует.");
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            
            await _userManager.RemoveFromRolesAsync(user, userRoles);
            await _userManager.AddToRoleAsync(user, Enum.GetName(typeof(UserRoles), role));

            await _context.SaveChangesAsync(cancellationToken);

            return ActionOutput.Success;
        }
    }
}