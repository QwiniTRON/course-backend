using System.Threading.Tasks;
using course_backend.Identity;
using course_backend.Implementations;
using Domain.Abstractions.Services;
using Domain.Enums;
using Domain.UseCases.User.Bann;
using Domain.UseCases.User.ChangeNick;
using Domain.UseCases.User.ChangeRole;
using Domain.UseCases.User.GetUsers;
using Domain.UseCases.User.UserInfo;
using Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace course_backend.Controllers
{
    [Route("api/v1/user")]
    public class UserController: Controller
    {
        private readonly IUseCaseDispatcher _dispatcher;
        private readonly ICurrentUserProvider _currentUserProvider;

        public UserController(IUseCaseDispatcher dispatcher, ICurrentUserProvider currentUserProvider)
        {
            _dispatcher = dispatcher;
            _currentUserProvider = currentUserProvider;
        }

        /// <summary>
        ///     Ban user
        /// </summary>
        /// <remarks>
        ///     # Ban user
        ///     ### access - only admin
        /// </remarks>
        [HttpPut("ban")]
        [AuthorizeByRole(UserRoles.Admin)]
        public async Task<IActionResult> BanUser([FromBody] BannInput request)
        {
            return await _dispatcher.DispatchAsync(request);
        }
        
        /// <summary>
        ///     Change user role
        /// </summary>
        /// <remarks>
        ///     # Change user role
        ///     ### access - only admin
        /// </remarks>
        [HttpPut("role")]
        [AuthorizeByRole(UserRoles.Admin)]
        public async Task<IActionResult> ChangeRole([FromBody] ChangeRoleInput request)
        {
            request.User = await _currentUserProvider.GetCurrentUser();
            return await _dispatcher.DispatchAsync(request);
        }

        /// <summary>
        ///     Change user nick
        /// </summary>
        /// <remarks>
        ///     # Change user nick
        /// </remarks>
        [HttpPut("nick")]
        [AuthorizeByRole(UserRoles.Admin, UserRoles.Participant, UserRoles.Teacher)]
        public async Task<IActionResult> ChangeNick([FromBody] ChangeNickInput request)
        {
            request.UserId = (await _currentUserProvider.GetCurrentUser()).Id;
            return await _dispatcher.DispatchAsync(request);
        }
        
        /// <summary>
        ///     Get full user info for current user(client)
        /// </summary>
        /// <remarks>
        ///     # Get full user info for current user(client)
        ///     ## with authorization data
        /// </remarks>
        [HttpGet("current")]
        [AuthorizeByRole(UserRoles.Admin, UserRoles.Participant, UserRoles.Teacher)]
        public async Task<IActionResult> GetCurrentUesrInfo()
        {
            var request = new UserInfoInput();
            request.UserId = (await _currentUserProvider.GetCurrentUser()).Id;
            return await _dispatcher.DispatchAsync(request);
        }
        
        /// <summary>
        ///     Get full user info by id
        /// </summary>
        /// <remarks>
        ///     # Get full user info by id
        /// </remarks>
        [HttpGet("{id}")]
        [AuthorizeByRole(UserRoles.Admin, UserRoles.Participant, UserRoles.Teacher)]
        public async Task<IActionResult> GetInfo([FromRoute]int id)
        {
            return await _dispatcher.DispatchAsync(new UserInfoInput() {UserId = id});
        }

        /// <summary>
        ///     Get all users
        /// </summary>
        /// <remarks>
        ///     # Get all users
        /// </remarks>
        [HttpGet]
        [AuthorizeByRole(UserRoles.Admin, UserRoles.Participant, UserRoles.Teacher)]
        public async Task<IActionResult> GetUsers([FromQuery]GetUsersInput request)
        {
            return await _dispatcher.DispatchAsync(request);
        }
    }
}