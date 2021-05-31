using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.PracticeOrder.GetMany
{
    public class GetPracticesCase: IUseCase<GetPracticesInput>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetPracticesCase(IAppDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        

        public async Task<IOutput> Handle(GetPracticesInput request, CancellationToken cancellationToken)
        {
            var search = request.Search ?? "";
            var limit = request.Limit ?? 16;
            var page = request.Page ?? 1;

            var practicesCount = await _context.PracticeOrders.AsNoTracking()
                .Include(x => x.Author)
                .Include(x => x.Lesson)
                .Where(x => x.IsDone == false && x.Author.Nick.Contains(search))
                .OrderBy(x => x.CreatedDate)
                .CountAsync(cancellationToken);
            
            _httpContextAccessor.HttpContext.Response.Headers.Add("X-Total-Count", practicesCount.ToString());
            
            var practices = await _context.PracticeOrders
                .AsNoTracking()
                .Include(x => x.Author)
                .Include(x => x.Lesson)
                .Where(x => x.IsDone == false && x.Author.Nick.Contains(search))
                .OrderBy(x => x.CreatedDate)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync(cancellationToken);

            return ActionOutput.SuccessData(
                _mapper.Map<List<Entity.PracticeOrder>, List<GetPracticesOutput>>(practices));
        }
    }
}