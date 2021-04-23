using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.PracticeOrder.OneInfo
{
    public class GetPracticeInfoCase: IUseCase<GetPracticeInfoInput>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetPracticeInfoCase(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        

        public async Task<IOutput> Handle(GetPracticeInfoInput request, CancellationToken cancellationToken)
        {
            var practice = await _context.PracticeOrders
                .Include(x => x.Author)
                .Include(x => x.Teacher)
                .Include(x => x.Lesson)
                .Include(x => x.PracticeContent)
                .FirstOrDefaultAsync(x => x.Id == request.PracticeId, cancellationToken);

            if (practice is null)
            {
                return ActionOutput.Error("Заявка на прохождение не нашлась");
            }
            
            return ActionOutput.SuccessData(_mapper.Map<Entity.PracticeOrder, GetPracticeInfoOutput>(practice));
        }
    }
}