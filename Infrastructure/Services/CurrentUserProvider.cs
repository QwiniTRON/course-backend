using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Abstractions.Services;
using Domain.Data;
using Domain.Entity;
using Domain.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class CurrentUserProvider : ICurrentUserProvider
    {
        private readonly IAppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserProvider(IAppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<User> GetCurrentUser()
        {
            var query = _context.Users
                .WithRoles();

            var id = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (string.IsNullOrEmpty(id))
                return null;

            if (!int.TryParse(id, out var intId))
                return null;

            return await query.FirstOrDefaultAsync(x => x.Id == intId);
        }
    }
}