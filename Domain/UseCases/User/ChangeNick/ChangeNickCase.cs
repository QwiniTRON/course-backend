using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.User.ChangeNick
{
    public class ChangeNickCase: IUseCase<ChangeNickInput>
    {
        private IAppDbContext _context;

        public ChangeNickCase(IAppDbContext context)
        {
            _context = context;
        }
        

        public async Task<IOutput> Handle(ChangeNickInput request, CancellationToken cancellationToken)
        {
            var currentUser = await _context.Users.FirstOrDefaultAsync(
                x => x.Id == request.UserId, cancellationToken: cancellationToken
                );

            if (currentUser is null)
            {
                return ActionOutput.Error("User was not found");
            }

            currentUser.Nick = request.NewNick;

            await _context.SaveChangesAsync(cancellationToken);

            return ActionOutput.Success;
        }
    }
}