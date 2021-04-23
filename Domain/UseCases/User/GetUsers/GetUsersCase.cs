using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.User.GetUsers
{
    public class GetUsersCase: IUseCase<GetUsersInput>
    {
        private IAppDbContext _context;

        public GetUsersCase(IAppDbContext context)
        {
            _context = context;
        }


        public async Task<IOutput> Handle(GetUsersInput request, CancellationToken cancellationToken)
        {
            var search = request.Search ?? "";
            var page = request.Page ?? 1;
            var limit = request.Limit ?? 16;

            if (String.IsNullOrEmpty(search))
            {
                return ActionOutput.Error("Search was null");
            }
            
            var users = await _context.Users
                .AsNoTracking()
                .Where(x => x.Mail.Contains(search) || x.Nick.Contains(search))
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync(cancellationToken: cancellationToken);

            return ActionOutput.SuccessData(new {users = users});
        }
    }
}