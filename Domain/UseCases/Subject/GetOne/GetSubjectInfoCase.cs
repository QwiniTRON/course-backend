using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Data;
using Domain.Views.Subject;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.Subject.GetOne
{
    public class GetSubjectInfoCase: IUseCase<GetSubjectInfoInput>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetSubjectInfoCase(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        
        public async Task<IOutput> Handle(GetSubjectInfoInput request, CancellationToken cancellationToken)
        {
            var subject = await _context.Subjects
                .Include(x => x.Lessons)
                .FirstOrDefaultAsync(x => x.Id == request.SubjectId || x.Name == request.SubjectName,
                    cancellationToken: cancellationToken);
            if (subject is null)
            {
                return ActionOutput.Error("Предмета не существует");
            }

            return ActionOutput.SuccessData(_mapper.Map<SubjectDeteiledView>(subject));
        }
    }
}