using System.Threading.Tasks;
using course_backend.Identity;
using course_backend.Implementations;
using Domain.Enums;
using Domain.UseCases.Comment.Add;
using Domain.UseCases.Comment.Delete;
using Domain.UseCases.Comment.Edit;
using Domain.UseCases.Comment.GetByLesson;
using Domain.UseCases.Lesson;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace course_backend.Controllers
{
    [Route("api/v1/comment")]
    public class CommentController: Controller
    {
        private readonly IUseCaseDispatcher _dispatcher;

        public CommentController(IUseCaseDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }
        
        
        /* add comment */
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateComment([FromBody]AddCommentInput request)
        {
            return await _dispatcher.DispatchAsync(request);
        } 
        
        /* delete */
        [HttpDelete("{id}")]
        [AuthorizeByRole(UserRoles.Admin, UserRoles.Teacher)]
        public async Task<IActionResult> DeleteComment(DeleteCommentInput request, [FromRoute]int id)
        {
            request.CommentId = id;
            return await _dispatcher.DispatchAsync(request);
        } 
        
        /* update */
        [HttpPut("{id}")]
        [AuthorizeByRole(UserRoles.Admin, UserRoles.Teacher)]
        public async Task<IActionResult> UpdateComment([FromBody]EditCommentInput request, [FromRoute]int id)
        {
            request.CommentId = id;
            return await _dispatcher.DispatchAsync(request);
        } 
        
        
        /* get by lesson */
        [HttpGet("lesson/{id}")]
        [AuthorizeByRole(UserRoles.Admin, UserRoles.Teacher)]
        public async Task<IActionResult> GetByLesson(CommentByLessonInput request, [FromRoute]int id)
        {
            request.LessonId= id;
            return await _dispatcher.DispatchAsync(request);
        } 
    }
}