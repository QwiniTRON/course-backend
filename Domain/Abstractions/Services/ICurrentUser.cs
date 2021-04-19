using System.Threading.Tasks;
using Domain.Entity;

namespace Domain.Abstractions.Services
{
    public interface ICurrentUserProvider
    {
        Task<User> GetCurrentUser();
    }
}