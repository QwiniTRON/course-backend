using System;
using System.Linq;
using System.Threading.Tasks;
using course_backend.Identity;
using course_backend.Implementations;
using Domain.UseCases.Progress.Add;
using Domain.UseCases.User.UserInfo;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace course_backend.Controllers
{
    [Route("api/v1/progress")]
    public class UserProgressController: Controller
    {
        private readonly IUseCaseDispatcher _dispatcher;
        private AppDbContext _context;

        public UserProgressController(AppDbContext context, IUseCaseDispatcher dispatcher)
        {
            _context = context;
            _dispatcher = dispatcher;
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddProgress([FromBody]AddProgressInput request)
        {
            int userId;
            int.TryParse(Request.HttpContext.User.Claims.First(x => x.Type == AppClaim.UserIdClaimName).Value, out userId);

            request.UserId = userId;
            
            return await _dispatcher.DispatchAsync(request);
        }
    }
}