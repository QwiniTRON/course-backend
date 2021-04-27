using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Data;
using Domain.Extensions;
using Domain.Maps.Views;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.User.UserInfo
{
    public class UesrInfoCase: IUseCase<UserInfoInput>
    {
        private IAppDbContext _context;
        private IMapper _mapper;

        public UesrInfoCase(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        
        public async Task<IOutput> Handle(UserInfoInput request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .AsNoTracking()
                .WithRoles()
                .Include(x => x.SubjectSertificates)
                .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken: cancellationToken);
            
            if (user is null)
            {
                return ActionOutput.Error("Пользователь не был найден");
            }

            return ActionOutput.SuccessData(_mapper.Map<UserViewDetailed>(user));
        }
    }
}