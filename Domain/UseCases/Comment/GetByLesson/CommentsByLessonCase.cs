using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Data;
using Domain.Maps.Views.Comment;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.Comment.GetByLesson
{
    public class CommentsByLessonCase: IUseCase<CommentByLessonInput>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public CommentsByLessonCase(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        

        public async Task<IOutput> Handle(CommentByLessonInput request, CancellationToken cancellationToken)
        {
            var comments = await _context.Comments.AsNoTracking()
                .Include(x => x.Lesson)
                .Where(x => x.Lesson.Id == request.LessonId)
                .ToListAsync(cancellationToken: cancellationToken);
            
            return ActionOutput.SuccessData(_mapper.Map<List<CommentView>>(comments));
        }
    }
}