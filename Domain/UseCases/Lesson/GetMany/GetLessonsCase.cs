using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Data;
using Domain.Maps.Views;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.Lesson
{
    public class GetLessonsCase: IUseCase<GetLessonsInput>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetLessonsCase(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        
        public async Task<IOutput> Handle(GetLessonsInput request, CancellationToken cancellationToken)
        {
            var lessons = await _context.Lessons.ToListAsync(cancellationToken: cancellationToken);
            
            return ActionOutput.SuccessData(_mapper.Map<List<Entity.Lesson>, List<LessonView>>(lessons));
        }
    }
}