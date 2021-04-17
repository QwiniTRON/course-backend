using System.Linq;
using Domain.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace course_backend.Identity
{
    public class AuthorizeByRoleAttribute: AuthorizeAttribute
    {
        public AuthorizeByRoleAttribute(params UserRoles[] roles)
        {
            Roles = string.Join(", ", roles.Select(r => r.ToString()));
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }
    }
}