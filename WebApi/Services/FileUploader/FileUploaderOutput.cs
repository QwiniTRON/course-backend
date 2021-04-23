using Domain.Abstractions.Outputs;
using Domain.Abstractions.Services;

namespace course_backend.Services
{
    public class FileUploaderOutput : IFileUploaderOutput
    {
        public FileUploaderOutput(string operatedFilePath, string operatedFileRelatedPath)
        {
            OperatedFilePath = operatedFilePath;
            OperatedFileRelatedPath = operatedFileRelatedPath;
        }

        public string OperatedFilePath { get; set; }
        public string OperatedFileRelatedPath { get; set; }
    }
}