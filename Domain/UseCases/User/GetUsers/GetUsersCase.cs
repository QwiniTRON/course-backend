using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Data;
using Domain.Extensions;
using Domain.Maps.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.User.GetUsers
{
    public class GetUsersCase: IUseCase<GetUsersInput>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetUsersCase(IAppDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<IOutput> Handle(GetUsersInput request, CancellationToken cancellationToken)
        {
            var search = request.Search ?? "";
            var page = request.Page ?? 1;
            var limit = request.Limit ?? 16;
            
            var usersCount = await _context.Users
                .AsNoTracking()
                .WithRoles()
                .Include(x => x.SubjectSertificates)
                .Where(x => x.Mail.Contains(search) || x.Nick.Contains(search))
                .CountAsync(cancellationToken: cancellationToken);
            
            _httpContextAccessor.HttpContext.Response.Headers.Add("X-Total-Count", usersCount.ToString());
            
            var users = await _context.Users
                .AsNoTracking()
                .WithRoles()
                .Include(x => x.SubjectSertificates)
                .Where(x => x.Mail.Contains(search) || x.Nick.Contains(search))
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync(cancellationToken: cancellationToken);

            return ActionOutput.SuccessData(_mapper.Map<List<Entity.User>, List<UserViewDetailed>>(users));
        }
    }
}