using Domain.Abstractions.Outputs;
using Domain.Abstractions.Services;

namespace course_backend.Services
{
    public class FileUploaderOutput : IFileUploaderOutput
    {
        public FileUploaderOutput(string operatedFilePath)
        {
            OperatedFilePath = operatedFilePath;
        }

        public string OperatedFilePath { get; set; }
    }
}