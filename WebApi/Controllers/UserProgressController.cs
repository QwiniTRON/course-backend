using System;
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
        
        [HttpGet("current")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetProgressCurrent()
        {
            var request = new ProgressesByIdInput();
            
            var currentUser = await _currentUserProvider.GetCurrentUser();

            if (currentUser is null)
            {
                return Json(ActionOutput.Error("Пользователь не найден"));
            }
            
            request.UserId = currentUser.Id;
            
            return await _dispatcher.DispatchAsync(request);
        }
        
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetProgress([FromRoute]int id)
        {
            var request = new ProgressesByIdInput();
            
            request.UserId = id;
            
            return await _dispatcher.DispatchAsync(request);
        }
    }
}