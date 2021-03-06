using System;
using System.IO;
using System.Threading.Tasks;
using Domain.Abstractions.Outputs;
using Domain.Abstractions.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace course_backend.Services
{
    public class FileUploader : IFileUploader
    {
        public string FileFolderDefault { get; private set; } = "files";
        
        private IWebHostEnvironment _appEnvironment;

        public FileUploader(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        /* save */
        public async Task<IOperationResult<IFileUploaderOutput>> SaveFile(IFormFile file, string path = null)
        {
            string fileUniqName =  GetUniqFileName(file.FileName);
            path ??=  Path.Combine(_appEnvironment.WebRootPath, FileFolderDefault, fileUniqName);

            try
            {
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            catch (Exception e)
            {
                return OperationResult<FileUploaderOutput>.ErrorData(e.ToString(), e);
            }
            
            return OperationResult<FileUploaderOutput>
                .SuccessData(new FileUploaderOutput(path, fileUniqName));
        }

        /* delete */
        public async Task<IOperationResult<IFileUploaderOutput>> DeleteFile(string fileRelatedPath,string path = null)
        {
            path ??=  Path.Combine(_appEnvironment.WebRootPath, FileFolderDefault, fileRelatedPath);
            
            try
            {
                File.Delete(path);
            }
            catch (Exception e)
            {
                return OperationResult<FileUploaderOutput>.ErrorData(e.ToString(), e);
            }
            
            return OperationResult<FileUploaderOutput>.SuccessData(new FileUploaderOutput(path, fileRelatedPath));
        }
        
        public async Task<byte[]> GetAsync(string filename)
        {
            try {
                var path = Path.Combine(_appEnvironment.WebRootPath, FileFolderDefault, filename);
                var bytes = await File.ReadAllBytesAsync(path);

                return bytes;
            }
            catch {
                return new byte[0];
            }
        }
        
        public FileStream GetFileStream(string filename)
        {
            try {
                var path = Path.Combine(_appEnvironment.WebRootPath, FileFolderDefault, filename);
                if(File.Exists(path) == false) throw(new NullReferenceException("file isn't found"));
                return File.OpenRead(path);
            }
            catch {
                throw(new NullReferenceException("file isn't found"));
            }
        }

        private string GetUniqFileName(string fileName) => DateTime.Now.ToFileTimeUtc().ToString() + "_" + fileName;
        
        public string SetFileFolderDefault(string folderPath)
        {
            FileFolderDefault = folderPath;
            return folderPath;
        }
    }
}