using System.Collections.Generic;
using System.Threading.Tasks;
using course_backend.Identity;
using course_backend.Implementations;
using Domain.Abstractions.Outputs;
using Domain.Abstractions.Services;
using Domain.Enums;
using Domain.UseCases.PracticeOrder.Add;
using Domain.UseCases.PracticeOrder.CurrentUserPractices;
using Domain.UseCases.PracticeOrder.GetMany;
using Domain.UseCases.PracticeOrder.GetOne;
using Domain.UseCases.PracticeOrder.OneInfo;
using Domain.UseCases.PracticeOrder.Reject;
using Domain.UseCases.PracticeOrder.Resolve;
using Domain.Views.PracticeOrder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace course_backend.Controllers
{
    [Route("api/v1/practice")]
    public class PracticeOrderController: Controller
    {
        private readonly IUseCaseDispatcher _dispatcher;
        private readonly ICurrentUserProvider _currentUserProvider;

        public PracticeOrderController(IUseCaseDispatcher dispatcher, ICurrentUserProvider currentUserProvider)
        {
            _dispatcher = dispatcher;
            _currentUserProvider = currentUserProvider;
        }

        /// <summary>
        ///     Add order to complete a practice lesson for current user
        /// </summary>
        /// <remarks>
        ///     # Create new order to complete a practice lesson for current user
        /// </remarks>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddPracticeOrder([FromForm]AddPracticeOrderInput request)
        {
            var currentUser = await _currentUserProvider.GetCurrentUser();
            if (currentUser is null) return Json(ActionOutput.Error("Пользователь не найден")); 
            request.UserId = currentUser.Id;
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
        [ProducesResponseType(typeof(List<GetPracticesOutput>), 200)]
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
        /// </remarks>
        [HttpGet("user/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(List<GetOnePracticeOutput>), 200)]
        public async Task<IActionResult> GetOneByUserId(GetOnePracticeInput request, [FromRoute]int id)
        {
            request.UserId = id;
            return await _dispatcher.DispatchAsync(request);
        }
        
        [HttpGet("current")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(List<PracticeOrderView>), 200)]
        public async Task<IActionResult> GetAllPracticesForCurrentUser(CurrentUserPracticesInput request)
        {
            var currentUser = await _currentUserProvider.GetCurrentUser();
            if (currentUser is null) return Json(ActionOutput.Error("Пользователь не найден")); 
            request.UserId = currentUser.Id;
            
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
        [ProducesResponseType(typeof(GetPracticeInfoOutput), 200)]
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
        ///     ## teacher is current user
        /// </remarks>
        [HttpPut("resolve")]
        [AuthorizeByRole(UserRoles.Teacher, UserRoles.Admin)]
        public async Task<IActionResult> Resolve([FromBody]ResolvePracticeInput request)
        {
            var currentUser = await _currentUserProvider.GetCurrentUser();
            if (currentUser is null) return Json(ActionOutput.Error("Пользователь не найден")); 
            request.TeacherId = currentUser.Id;
            return await _dispatcher.DispatchAsync(request);
        }
        
        /// <summary>
        ///     Reject order
        /// </summary>
        /// <remarks>
        ///     # Reject order
        ///     ## teacher is current user
        /// </remarks>
        [HttpPut("reject")]
        [AuthorizeByRole(UserRoles.Teacher, UserRoles.Admin)]
        public async Task<IActionResult> Reject([FromBody]RejectPracticeInput request)
        {
            var currentUser = await _currentUserProvider.GetCurrentUser();
            if (currentUser is null) return Json(ActionOutput.Error("Пользователь не найден")); 
            request.TeacherId = currentUser.Id;
            return await _dispatcher.DispatchAsync(request);
        }
    }
}