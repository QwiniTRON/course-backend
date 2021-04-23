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
        public string FileFolderDefault { get; private set; } = "/files/";
        
        private IWebHostEnvironment _appEnvironment;

        public FileUploader(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        /* save */
        public async Task<IOperationResult<IFileUploaderOutput>> SaveFile(IFormFile file, string path = null)
        {
            path ??=  _appEnvironment.WebRootPath + FileFolderDefault + file.FileName;

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
            
            return OperationResult<FileUploaderOutput>.SuccessData(new FileUploaderOutput(path));
        }

        /* delete */
        public async Task<IOperationResult<IFileUploaderOutput>> DeleteFile(string fileName,string path = null)
        {
            path ??=  _appEnvironment.WebRootPath + FileFolderDefault + fileName;
            
            try
            {
                File.Delete(path);
            }
            catch (Exception e)
            {
                return OperationResult<FileUploaderOutput>.ErrorData(e.ToString(), e);
            }
            
            return OperationResult<FileUploaderOutput>.SuccessData(new FileUploaderOutput(path));
        }

        public string SetFileFolderDefault(string folderPath)
        {
            FileFolderDefault = folderPath;
            return folderPath;
        }
    }
}