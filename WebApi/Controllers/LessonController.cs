using System.Collections.Generic;
using System.Threading.Tasks;
using course_backend.Identity;
using course_backend.Implementations;
using Domain.Enums;
using Domain.Maps.Views;
using Domain.UseCases.Lesson;
using Domain.UseCases.Lesson.AddLesson;
using Domain.UseCases.Lesson.DeleteLesson;
using Domain.UseCases.Lesson.EditLesson;
using Domain.UseCases.Lesson.GetOne;
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

        /// <summary>
        ///     Get all lessons
        /// </summary>
        /// <remarks>
        ///     # Get all lessons
        /// </remarks>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(List<LessonView>), 200)]
        public async Task<IActionResult> GetLessons(GetLessonsInput request)
        {
            return await _dispatcher.DispatchAsync(request);
        } 
        
        /// <summary>
        ///     Get lesson by id
        /// </summary>
        /// <remarks>
        ///     # Get lesson by id
        /// </remarks>
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(LessonDetailedView), 200)]
        public async Task<IActionResult> GetLessons(LessonGetOneInput request, [FromRoute]int id)
        {
            request.SetLessonId(id);
            return await _dispatcher.DispatchAsync(request);
        } 
        
        [HttpDelete("{id}")]
        [AuthorizeByRole(UserRoles.Admin)]
        public async Task<IActionResult> DeleteLesson(DeleteLessonInput request, [FromRoute]int id)
        {
            request.LessonId = id;
            return await _dispatcher.DispatchAsync(request);
        } 
        
        [HttpPut("{id}")]
        [AuthorizeByRole(UserRoles.Admin, UserRoles.Teacher)]
        public async Task<IActionResult> EditLesson([FromBody]EditLessonInput request, [FromRoute]int id)
        {
            request.LessonId = id;
            return await _dispatcher.DispatchAsync(request);
        } 
        
        [HttpPost]
        [AuthorizeByRole(UserRoles.Admin, UserRoles.Teacher)]
        public async Task<IActionResult> CreateLesson([FromBody]AddLessonInput request)
        {
            return await _dispatcher.DispatchAsync(request);
        } 
    }
}