using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.User.ChangeRole
{
    public class ChangeRoleCase: IUseCase<ChangeRoleInput>
    {
        private IAppDbContext _context;

        public ChangeRoleCase(IAppDbContext context)
        {
            _context = context;
        }

        
        public async Task<IOutput> Handle(ChangeRoleInput request, CancellationToken cancellationToken)
        {
            Entity.User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken: cancellationToken);

            if (user is null)
            {
                return ActionOutput.Error("User wasn't found");
            }

            user.Role = request.NewRole;

            await _context.SaveChangesAsync(cancellationToken);

            return ActionOutput.Success;
        }
    }
}