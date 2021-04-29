using System.Threading.Tasks;
using course_backend.Identity;
using course_backend.Implementations;
using Domain.Enums;
using Domain.UseCases.Account.Check;
using Domain.UseCases.Account.SignIn;
using Domain.UseCases.Account.SignUp;
using Microsoft.AspNetCore.Mvc;

namespace course_backend.Controllers
{
    
    [Route("api/v1/account")]
    public class AccountController: Controller
    {
        private readonly IUseCaseDispatcher _dispatcher;
        
        public AccountController(IUseCaseDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        /// <summary>
        ///     login endpoint
        /// </summary>
        /// <remarks>
        ///     ## Response: token to authenticate
        /// </remarks>
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody]SignInInput request)
        {
            return await _dispatcher.DispatchAsync(request);
        }

        /// <summary>
        ///     registration endpoint
        /// </summary>
        /// <remarks>
        ///     ## Response: token to authenticate
        /// </remarks>
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromForm]SignUpInput request)
        {
            return await _dispatcher.DispatchAsync(request);
        }

        /// <summary>
        ///     check user authenticate and update token
        /// </summary>
        /// <remarks>
        ///     ## Response: token to authenticate
        /// </remarks>
        [HttpPost("check")]
        [AuthorizeByRole(UserRoles.Admin, UserRoles.Participant, UserRoles.Teacher)]
        public async Task<IActionResult> Check(CheckInput request)
        {
            return await _dispatcher.DispatchAsync(request);
        }
    }
}