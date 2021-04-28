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

        public ChangeRoleCase(IAppDbContext context, UserManager<Entity.User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        
        public async Task<IOutput> Handle(ChangeRoleInput request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .WithRoles()
                .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken: cancellationToken);
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