namespace Domain.Abstractions.Services.ITemplateCreator
{
    public interface ITemplateCreator
    {
        string GetRejectLetter(string lessonName, string rejectDescription);
        public string GetResolveLetter(string lessonName);
    }
}