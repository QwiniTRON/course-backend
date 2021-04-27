using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Data;
using Domain.Maps.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Domain.UseCases.Lesson.GetOne
{
    public class LessonGetOneCase: IUseCase<LessonGetOneInput>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<LessonGetOneCase> _logger;
        

        public LessonGetOneCase(IAppDbContext context, ILogger<LessonGetOneCase> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        
        public async Task<IOutput> Handle(LessonGetOneInput request, CancellationToken cancellationToken)
        {
            Entity.Lesson lesson = await _context.Lessons
                .Include(x => x.Comments)
                .Include(x => x.Subject)
                .FirstOrDefaultAsync(x => x.Id == request.LessonId, 
                    cancellationToken: cancellationToken);

            if (lesson is null)
            {
                _logger.LogInformation("Lesson with id {id} wasn't found", request.LessonId);
                return ActionOutput.Error("Урок не существует.");
            }
            
            return ActionOutput.SuccessData(_mapper.Map<LessonDetailedView>(lesson));
        }
    }
}