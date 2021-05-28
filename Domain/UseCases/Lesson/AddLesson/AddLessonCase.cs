using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions.Mediatr;
using Domain.Abstractions.Outputs;
using Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Domain.UseCases.Lesson.AddLesson
{
    public class AddLessonCase: IUseCase<AddLessonInput>
    {
        private readonly IAppDbContext _context;

        public AddLessonCase(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<IOutput> Handle(AddLessonInput request, CancellationToken cancellationToken)
        {
            var reactSubject = await _context.Subjects.FirstOrDefaultAsync((x) => x.Name == "React.js");

            var lesson = new Entity.Lesson(
                request.Name, 
                request.IsPractice, 
                reactSubject, 
                request.Index, 
                request.Description, 
                request.Content);

            _context.Lessons.Add(lesson);

            await _context.SaveChangesAsync(cancellationToken);
            
            return ActionOutput.SuccessData(lesson.Id);
        }
    }
}