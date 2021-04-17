using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.User.Bann
{
    public class BannCase: IUseCase<BannInput>
    {
        private IAppDbContext _context;

        public BannCase(IAppDbContext context)
        {
            _context = context;
        }

        
        public async Task<IOutput> Handle(BannInput request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserForBannId, cancellationToken: cancellationToken);

            if (user is null)
            {
                return ActionOutput.Error("User wasn't found");
            }

            user.IsBanned = true;

            await _context.SaveChangesAsync(cancellationToken);
            
            return ActionOutput.Success;
        }
    }
}