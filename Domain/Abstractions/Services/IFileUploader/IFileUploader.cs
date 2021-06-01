using System.IO;
using System.Threading.Tasks;
using Domain.Abstractions.Outputs;
using Microsoft.AspNetCore.Http;

namespace Domain.Abstractions.Services
{
    public interface IFileUploader
    {
        Task<IOperationResult<IFileUploaderOutput>> SaveFile(IFormFile file, string path = null);
        Task<IOperationResult<IFileUploaderOutput>> DeleteFile(string fileRelatedPath,string path = null);
        Task<byte[]> GetAsync(string filename);
        FileStream GetFileStream(string filename);
    }
}