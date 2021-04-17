using System.Threading.Tasks;
using course_backend.Identity;
using course_backend.Implementations;
using Domain.Enums;
using Domain.UseCases.User.Bann;
using Domain.UseCases.User.ChangeNick;
using Domain.UseCases.User.ChangeRole;
using Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace course_backend.Controllers
{
    [Route("api/v1/user")]
    public class UserController: Controller
    {
        private readonly IUseCaseDispatcher _dispatcher;
        private AppDbContext _context;
        private readonly IMediator _mediator;

        public UserController(IUseCaseDispatcher dispatcher, AppDbContext context, IMediator mediator)
        {
            _dispatcher = dispatcher;
            _context = context;
            _mediator = mediator;
        }

        [Authorize]
        [AuthorizeByRole(UserRoles.Admin)]
        public async Task<IActionResult> BannUser([FromBody] BannInput request)
        {
            return await _dispatcher.DispatchAsync(request);
        }
        
        [Authorize]
        [AuthorizeByRole(UserRoles.Admin)]
        public async Task<IActionResult> ChangeRole([FromBody] ChangeRoleInput request)
        {
            return await _dispatcher.DispatchAsync(request);
        }
        
        [Authorize]
        public async Task<IActionResult> ChangeNick([FromBody] ChangeNickInput request)
        {
            if (HttpContext.User.Identity != null) request.CurrentUserMail = HttpContext.User.Identity.Name;
            return await _dispatcher.DispatchAsync(request);
        }
    }
}