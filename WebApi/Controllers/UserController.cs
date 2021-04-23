﻿using System.Threading.Tasks;
using course_backend.Identity;
using course_backend.Implementations;
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

        public UserController(IUseCaseDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        /* ban user */
        [HttpPut("ban")]
        [AuthorizeByRole(UserRoles.Admin)]
        public async Task<IActionResult> BannUser([FromBody] BannInput request)
        {
            return await _dispatcher.DispatchAsync(request);
        }
        
        /* change role */
        [HttpPut("role")]
        [AuthorizeByRole(UserRoles.Admin)]
        public async Task<IActionResult> ChangeRole([FromBody] ChangeRoleInput request)
        {
            return await _dispatcher.DispatchAsync(request);
        }

        /* change nick */
        [HttpPut("nick")]
        [AuthorizeByRole(UserRoles.Admin, UserRoles.Participant, UserRoles.Teacher)]
        public async Task<IActionResult> ChangeNick([FromBody] ChangeNickInput request)
        {
            if (HttpContext.User.Identity != null) request.CurrentUserMail = HttpContext.User.Identity.Name;
            return await _dispatcher.DispatchAsync(request);
        }
        
        /* get user info */
        [HttpGet("{id}")]
        [AuthorizeByRole(UserRoles.Admin, UserRoles.Participant, UserRoles.Teacher)]
        public async Task<IActionResult> GetInfo([FromRoute]int id)
        {
            return await _dispatcher.DispatchAsync(new UserInfoInput() {UserId = id});
        }
        
        /* get users */
        [HttpGet]
        [AuthorizeByRole(UserRoles.Admin, UserRoles.Participant, UserRoles.Teacher)]
        public async Task<IActionResult> GetUsers([FromQuery]GetUsersInput request)
        {
            return await _dispatcher.DispatchAsync(request);
        }
    }
}