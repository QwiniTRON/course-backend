using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using course_backend.Identity;
using course_backend.Implementations;
using course_backend.Services;
using Domain.Abstractions.Outputs;
using Domain.Abstractions.Services;
using Domain.Entity;
using Domain.Enums;
using Domain.UseCases.Account.Check;
using Domain.UseCases.Account.SignIn;
using Domain.UseCases.Account.SignUp;
using Infrastructure.Data;
using Infrastructure.Data.Configuration;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace course_backend.Controllers
{
    
    [Route("api/v1/account")]
    public class AccountController: Controller
    {
        private readonly AuthOptions _authOptions;
        private readonly AppDbContext _context;
        private readonly IMediator _mediator;
        private readonly IModelValidate _validate;
        private readonly IAuthDataProvider _dataProvider;
        private readonly IUseCaseDispatcher _dispatcher;
        

        public AccountController(IOptions<AuthOptions> authOptions, AppDbContext context, IMediator mediator, IModelValidate validate, IAuthDataProvider dataProvider, IUseCaseDispatcher dispatcher)
        {
            _authOptions = authOptions.Value;
            _context = context;
            _mediator = mediator;
            _validate = validate;
            _dataProvider = dataProvider;
            _dispatcher = dispatcher;
        }

        
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody]SignInInput request)
        {
            return await _dispatcher.DispatchAsync(request);
        }

        [HttpPost("signup")]
        // [AuthorizeByRole(UserRoles.Admin)]
        public async Task<IActionResult> SignUp([FromBody]SignUpInput request)
        {
            return await _dispatcher.DispatchAsync(request);
        }

        [HttpPost("check")]
        [Authorize]
        public async Task<IActionResult> Check(CheckInput request)
        {
            return await _dispatcher.DispatchAsync(request);
        }
    }
}