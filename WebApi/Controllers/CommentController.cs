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
        
        
        /// <summary>
        ///     Add new comment for lesson
        /// </summary>
        /// <remarks>
        ///     # add new comment to lesson
        /// </remarks>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateComment([FromBody]AddCommentInput request)
        {
            return await _dispatcher.DispatchAsync(request);
        } 
        
        /// <summary>
        ///     Delete comment by id
        /// </summary>
        /// <remarks>
        ///     # Delete comment by id
        /// </remarks>
        [HttpDelete("{id}")]
        [AuthorizeByRole(UserRoles.Admin, UserRoles.Teacher)]
        public async Task<IActionResult> DeleteComment([FromRoute]int id)
        {
            var request = new DeleteCommentInput { CommentId = id};
            return await _dispatcher.DispatchAsync(request);
        } 
        
        /// <summary>
        ///     Update comment by id
        /// </summary>
        /// <remarks>
        ///     # Update comment by id
        /// </remarks>
        [HttpPut("{id}")]
        [AuthorizeByRole(UserRoles.Admin, UserRoles.Teacher)]
        public async Task<IActionResult> UpdateComment([FromBody]EditCommentInput request, [FromRoute]int id)
        {
            request.SetCommentId(id);
            return await _dispatcher.DispatchAsync(request);
        } 
        
        
        /// <summary>
        ///     Get comments for lesson
        /// </summary>
        /// <remarks>
        ///     # Get comments for lesson
        /// </remarks>
        [HttpGet("lesson/{id}")]
        [AuthorizeByRole(UserRoles.Admin, UserRoles.Teacher)]
        public async Task<IActionResult> GetByLesson([FromRoute]int id)
        {
            var request = new CommentByLessonInput {LessonId = id};
            return await _dispatcher.DispatchAsync(request);
        } 
    }
}