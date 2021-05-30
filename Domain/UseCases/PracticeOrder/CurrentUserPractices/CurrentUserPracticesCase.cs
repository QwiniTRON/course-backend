using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Data;
using Domain.Views.PracticeOrder;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.PracticeOrder.CurrentUserPractices
{
    public class CurrentUserPracticesCase: IUseCase<CurrentUserPracticesInput>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public CurrentUserPracticesCase(IMapper mapper, IAppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IOutput> Handle(CurrentUserPracticesInput request, CancellationToken cancellationToken)
        {
            var practices = await _context.PracticeOrders
                .Include(x => x.PracticeContent)
                .Where(x => x.AuthorId == request.UserId)
                .ToListAsync(cancellationToken);

            return ActionOutput.SuccessData(_mapper.Map<List<PracticeOrderView>>(practices));
        }
    }
}