using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Data;
using Domain.Maps;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.Progress.ProgressesById
{
    public class ProgressesByIdCase: IUseCase<ProgressesByIdInput>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public ProgressesByIdCase(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        

        public async Task<IOutput> Handle(ProgressesByIdInput request, CancellationToken cancellationToken)
        {
            var progresses = await _context.Progresses
                .Include(x => x.Lesson)
                    .ThenInclude(x => x.Subject)
                .Include(x => x.User)
                .Where(x => x.UserId == request.UserId && x.Lesson.SubjectId == request.SubjectId)
                .ToListAsync(cancellationToken: cancellationToken);

            return ActionOutput.SuccessData(_mapper.Map<List<ProgressesByIdOutput>>(progresses));
        }
    }
}