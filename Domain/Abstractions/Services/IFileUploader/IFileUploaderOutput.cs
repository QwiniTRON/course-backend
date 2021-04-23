namespace Domain.Abstractions.Services
{
    public interface IFileUploaderOutput
    {
        string OperatedFilePath { get; set; }
        public string OperatedFileRelatedPath { get; set; }
    }
}