using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Abstractions.Services;
using Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.User.Bann
{
    public class BannCase: IUseCase<BannInput>
    {
        private IAppDbContext _context;
        private readonly ICurrentUserProvider _currentUserProvider;

        public BannCase(IAppDbContext context, ICurrentUserProvider currentUserProvider)
        {
            _context = context;
            _currentUserProvider = currentUserProvider;
        }

        
        public async Task<IOutput> Handle(BannInput request, CancellationToken cancellationToken)
        {
            var currentUesrId = (await _currentUserProvider.GetCurrentUser()).Id;
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserForBannId, cancellationToken: cancellationToken);

            if (user is null)
            {
                return ActionOutput.Error("User wasn't found");
            }
            
            if (user.Id == currentUesrId)
            {
                return ActionOutput.Error("нельзя банить себя");
            }

            user.IsBanned = !user.IsBanned;

            await _context.SaveChangesAsync(cancellationToken);
            
            return ActionOutput.Success;
        }
    }
}