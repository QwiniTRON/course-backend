using System.Security.Claims;

namespace Domain.Abstractions.Services
{
    public interface IAuthDataProvider
    {
        string GetJwtByIdentity(ClaimsIdentity identity);
        ClaimsIdentity GetIdentity(string username);
    }
}