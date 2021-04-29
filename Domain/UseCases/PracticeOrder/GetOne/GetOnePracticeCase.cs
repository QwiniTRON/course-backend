using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.PracticeOrder.GetOne
{
    public class GetOnePracticeCase: IUseCase<GetOnePracticeInput>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        

        public GetOnePracticeCase(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        

        public async Task<IOutput> Handle(GetOnePracticeInput request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken: cancellationToken);

            if (user is null)
            {
                return ActionOutput.Error("Пользователь не найден");
            }

            var practices = await _context.PracticeOrders
                .Include(x => x.Author)
                .Include(x => x.Teacher)
                .Where(x => x.Author.Id == user.Id && x.LessonId == request.LessonId)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync(cancellationToken: cancellationToken);

            return request.Last ? 
                ActionOutput.SuccessData(_mapper.Map<Entity.PracticeOrder, GetOnePracticeOutput>(practices.FirstOrDefault())) : 
                ActionOutput.SuccessData(_mapper.Map<List<Entity.PracticeOrder>, List<GetOnePracticeOutput>>(practices));
        }
    }
}