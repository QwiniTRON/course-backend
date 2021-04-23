using System.Threading.Tasks;
using course_backend.Implementations;
using Domain.UseCases.Lesson;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace course_backend.Controllers
{
    [Route("api/v1/lesson")]
    public class LessonController: Controller
    {
        private readonly IUseCaseDispatcher _dispatcher;

        public LessonController(IUseCaseDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetLessons(GetLessonsInput request)
        {
            return await _dispatcher.DispatchAsync(request);
        } 
    }
}