using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.User.UserInfo
{
    public class UesrInfoCase: IUseCase<UserInfoInput>
    {
        private IAppDbContext _context;
        private IMapper _mapper;

        public UesrInfoCase(IAppDbContext context)
        {
            _context = context;
        }

        
        public async Task<IOutput> Handle(UserInfoInput request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken: cancellationToken);

            if (user is null)
            {
                return ActionOutput.Error("User wasn't found");
            }

            return ActionOutput.SuccessData(_mapper.Map<Entity.User, UserInfoOutput>(user));
        }
    }
}