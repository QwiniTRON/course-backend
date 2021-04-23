using System;
using System.Threading.Tasks;

namespace Domain.Data
{
    public interface IUnitOfWork: IDisposable
    {
        Task Apply();
        Task Cancel();
    }
}