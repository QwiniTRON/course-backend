using System.Threading.Tasks;

namespace Domain.Abstractions
{
    public interface IFileStorage
    {
        Task<string> SaveFile(byte[] bytes, string extension);
        Task<byte[]> GetAsync(string filename);
    }
}