using System.Threading.Tasks;

namespace Infrastructure.Abstractions
{
    public interface IUnitOfWork
    {
        Task Apply();
        Task Cancel();
    }
}