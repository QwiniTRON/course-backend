using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using course_backend.Identity;
using course_backend.Implementations;
using Domain.Abstractions.Outputs;
using Domain.Abstractions.Services;
using Domain.UseCases.Progress.Add;
using Domain.UseCases.Progress.ProgressesById;
using Domain.UseCases.User.UserInfo;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace course_backend.Controllers
{
    [Route("api/v1/progress")]
    public class UserProgressController: Controller
    {
        private readonly IUseCaseDispatcher _dispatcher;
        private readonly ICurrentUserProvider _currentUserProvider;

        public UserProgressController(IUseCaseDispatcher dispatcher, ICurrentUserProvider currentUserProvider)
        {
            _dispatcher = dispatcher;
            _currentUserProvider = currentUserProvider;
        }
        
        /// <summary>
        ///     Create a new mark that lesson is done
        /// </summary>
        /// <remarks>
        ///     # Create a new mark that lesson is done
        /// </remarks>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddProgress([FromBody]AddProgressInput request)
        {
            var currentUser = await _currentUserProvider.GetCurrentUser();

            if (currentUser is null)
            {
                return Json(ActionOutput.Error("Пользователь не найден"));
            }
            
            request.UserId = currentUser.Id;
            
            return await _dispatcher.DispatchAsync(request);
        }
        
        /// <summary>
        ///     Get all progresses for user by subject id
        /// </summary>
        /// <remarks>
        ///     # Get all progresses for user by subject id
        /// </remarks>
        [HttpGet("current")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(List<ProgressesByIdOutput>), 200)]
        public async Task<IActionResult> GetProgressCurrent([FromQuery] ProgressesByIdInput request)
        {
            var currentUser = await _currentUserProvider.GetCurrentUser();

            if (currentUser is null)
            {
                return Json(ActionOutput.Error("Пользователь не найден"));
            }
            
            request.SetUserId(currentUser.Id);
            
            return await _dispatcher.DispatchAsync(request);
        }
        
        /// <summary>
        ///     Get all progresses for user by subject id
        /// </summary>
        /// <remarks>
        ///     # Get all progresses for user by user id and subject id
        /// </remarks>
        [HttpGet("{userid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(List<ProgressesByIdOutput>), 200)]
        public async Task<IActionResult> GetProgress([FromQuery] ProgressesByIdInput request, [FromRoute]int userid)
        {
            request.SetUserId(userid);
            
            return await _dispatcher.DispatchAsync(request);
        }
    }
}