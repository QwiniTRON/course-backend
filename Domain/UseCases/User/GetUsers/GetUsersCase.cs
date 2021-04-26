using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Data;
using Domain.Maps.Views;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.User.GetUsers
{
    public class GetUsersCase: IUseCase<GetUsersInput>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetUsersCase(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<IOutput> Handle(GetUsersInput request, CancellationToken cancellationToken)
        {
            var search = request.Search ?? "";
            var page = request.Page ?? 1;
            var limit = request.Limit ?? 16;
            
            var users = await _context.Users
                .AsNoTracking()
                .Where(x => x.Mail.Contains(search) || x.Nick.Contains(search))
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync(cancellationToken: cancellationToken);

            return ActionOutput.SuccessData(_mapper.Map<List<Entity.User>, List<UserView>>(users));
        }
    }
}