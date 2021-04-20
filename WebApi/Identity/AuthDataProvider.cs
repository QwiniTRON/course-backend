using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Domain.Abstractions.Services;
using Domain.Entity;
using Domain.Extensions;
using Infrastructure.Data;
using Infrastructure.Data.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace course_backend.Identity
{
    public class AuthDataProvider : IAuthDataProvider
    {
        private readonly AuthOptions _authOptions;
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;

        public AuthDataProvider(IOptions<AuthOptions> authOptions, AppDbContext context, UserManager<User> userManager)
        {
            _authOptions = authOptions.Value;
            _context = context;
            _userManager = userManager;
        }


        public string GetJwtByIdentity(ClaimsIdentity identity)
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

        public ClaimsIdentity GetIdentity(string username)
        {
            User user = _context.Users.WithRoles().FirstOrDefault(x=>x.Mail == username);

            if (user is null) return null; 
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                // new Claim(ClaimsIdentity.DefaultNameClaimType, user.Mail),
                // new Claim(ClaimTypes.Name, user.Id.ToString()),
                // new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Roles.First().ToString()),
                // new Claim(AppClaim.UserIdClaimName, user.Id.ToString()),
                // new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                // new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            claims.AddRange(user.Roles.Select(r => new Claim(ClaimTypes.Role, r.ToString())));
            
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