using System.Threading.Tasks;
using course_backend.Identity;
using course_backend.Implementations;
using Domain.Enums;
using Domain.UseCases.PracticeOrder.Add;
using Domain.UseCases.PracticeOrder.GetMany;
using Domain.UseCases.PracticeOrder.GetOne;
using Domain.UseCases.PracticeOrder.OneInfo;
using Domain.UseCases.PracticeOrder.Reject;
using Domain.UseCases.PracticeOrder.Resolve;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace course_backend.Controllers
{
    [Route("api/v1/practice")]
    public class PracticeOrderController: Controller
    {
        private readonly IUseCaseDispatcher _dispatcher;

        public PracticeOrderController(IUseCaseDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        /// <summary>
        ///     Add order to complete a practice lesson
        /// </summary>
        /// <remarks>
        ///     # Create new order to complete a practice lesson
        /// </remarks>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddPracticeOrder([FromForm]AddPracticeOrderInput request)
        {
            return await _dispatcher.DispatchAsync(request);
        }
        
        /// <summary>
        ///     Get practices to review
        /// </summary>
        /// <remarks>
        ///     get all practices for review
        /// </remarks>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetPracticeOrders([FromQuery]GetPracticesInput request)
        {
            return await _dispatcher.DispatchAsync(request);
        }
        
        /// <summary>
        ///     Get all practices orders for user by lesson
        /// </summary>
        /// <remarks>
        ///     # Get all user's practice orders by lesson
        ///     ### with param last returns last order for this lesson
        ///
        ///     ```
        ///         {
        ///             
        ///         }
        ///     ```
        /// </remarks>
        [HttpGet("user/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetOneByUserId(GetOnePracticeInput request, [FromRoute]int id)
        {
            request.SetUserId(id);
            return await _dispatcher.DispatchAsync(request);
        }
        
        /// <summary>
        ///     Get info for practice order by id
        /// </summary>
        /// <remarks>
        ///     # Get info for practice order by id
        /// </remarks>
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetOneInfo([FromRoute]int id)
        {
            var request = new GetPracticeInfoInput() {PracticeId = id};
            return await _dispatcher.DispatchAsync(request);
        }
        
        /// <summary>
        ///     Resolve order
        /// </summary>
        /// <remarks>
        ///     # Resolve order
        /// </remarks>
        [HttpPut("resolve")]
        [AuthorizeByRole(UserRoles.Teacher, UserRoles.Admin)]
        public async Task<IActionResult> Resolve([FromBody]ResolvePracticeInput request)
        {
            return await _dispatcher.DispatchAsync(request);
        }
        
        /// <summary>
        ///     Reject order
        /// </summary>
        /// <remarks>
        ///     # Reject order
        /// </remarks>
        [HttpPut("reject")]
        [AuthorizeByRole(UserRoles.Teacher, UserRoles.Admin)]
        public async Task<IActionResult> Reject([FromBody]RejectPracticeInput request)
        {
            return await _dispatcher.DispatchAsync(request);
        }
    }
}