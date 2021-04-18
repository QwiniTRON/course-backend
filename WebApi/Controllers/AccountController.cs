using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using course_backend.Identity;
using course_backend.Services;
using Domain.Abstractions.Outputs;
using Domain.Entity;
using Domain.Enums;
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
        private AuthOptions _authOptions;
        private AppDbContext _context;
        private readonly IMediator _mediator;
        private IModelValidate _validate;

        public AccountController(IOptions<AuthOptions> authOptions, AppDbContext context, IMediator mediator, IModelValidate validate)
        {
            _authOptions = authOptions.Value;
            _context = context;
            _mediator = mediator;
            _validate = validate;
        }

        
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody]SignInInput request)
        {
            var signinResult = await _mediator.Send(request);

            if (signinResult.Succeeded == false)
            {
                return Json(signinResult);
            }
            
            var identity = GetIdentity(request.Mail);
            
            if (identity is null)
            {
                return Json(ActionOutput.Error("Данные не верны"));
            }
            
            return Json(ActionOutput.SuccessData(new {token = GetJwtByIdentity(identity)}));
        }

        [HttpPost("signup")]
        // [AuthorizeByRole(UserRoles.Admin)]
        public async Task<IActionResult> SignUp([FromBody]SignUpInput request)
        {
            var validateResult = await _validate.Validate<SignUpInput>(
                    request, 
                    ControllerContext.HttpContext.RequestServices
                );

            if (validateResult.Succeeded == false)
            {
                return Json(ActionOutput.Error(validateResult.ErrorMessage));
            }
            
            
            IOutput registerResult = await _mediator.Send(request);

            if (!registerResult.Succeeded)
            {
                return Json(registerResult);
            }

            var identity = GetIdentity(request.Mail);

            if (identity is null)
            {
                return Json(ActionOutput.Error("Данные не верны"));
            }

            return Json(ActionOutput.SuccessData(new {token = GetJwtByIdentity(identity)}));
        }

        [HttpPost("check")]
        [Authorize]
        public IActionResult Check()
        {
            var identity = GetIdentity(User.Identity.Name);

            if (identity is null)
            {
                return Json(ActionOutput.Error("Данные не верны"));
            }
                
            return Json(ActionOutput.SuccessData(new {token = GetJwtByIdentity(identity)}));
        }

        private string GetJwtByIdentity(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: _authOptions.ISSUER, 
                audience: _authOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(_authOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(
                    _authOptions.GetSymmetricAlgorithmKey(), 
                    SecurityAlgorithms.HmacSha256
                )
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
        
        private ClaimsIdentity GetIdentity(string username)
        {
            User user = _context.Users.FirstOrDefault(x=>x.Mail == username);

            if (user is null) return null; 
            
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Mail),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString()),
                new Claim(AppClaim.UserIdClaimName, user.Id.ToString()),
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                    claims, 
                    "Token", 
                    ClaimsIdentity.DefaultNameClaimType, 
                    ClaimsIdentity.DefaultRoleClaimType
                );

            return claimsIdentity;
        }
    }
}