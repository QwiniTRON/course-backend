using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Domain.Abstractions.Services;
using Domain.Entity;
using Infrastructure.Data;
using Infrastructure.Data.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace course_backend.Identity
{
    public class AuthDataProvider : IAuthDataProvider
    {
        private AuthOptions _authOptions;
        private AppDbContext _context;

        public AuthDataProvider(AuthOptions authOptions, AppDbContext context)
        {
            _authOptions = authOptions;
            _context = context;
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