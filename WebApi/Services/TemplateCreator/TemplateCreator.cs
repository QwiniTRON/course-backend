using Domain.Abstractions.Services.ITemplateCreator;

namespace course_backend.Services.TemplateCreator
{
    public class TemplateCreator : ITemplateCreator
    {
        public TemplateCreator()
        {
        }

        public string GetRejectLetter(string lessonName, string rejectDescription)
        {
            return "<h2>Здравствуйте! Это Reacter, площадка для обучения.</h2>" +
                   "<div style=\"border: 2px solid bisque;padding: 15px;margin: 15px;\">" +
                   "   <div>" +
                   $"       <div>Урок {lessonName} был отклонен.</div>" +
                   $"       <p>По причине {rejectDescription}</p>" +
                   "   </div>" +
                   "</div>";
        }
        
        public string GetResolveLetter(string lessonName)
        {
            return "<h2>Здравствуйте! Это Reacter, площадка для обучения.</h2>" +
                   "<div style=\"border: 2px solid bisque;padding: 15px;margin: 15px;\">" +
                   "   <div>" +
                   $"       <div>Вы успешно прошли урок {lessonName}.</div>" +
                   "       <p>Продолжайте обучение. Мы ждём вас.</p>" +
                   "   </div>" +
                   "</div>";
        }
    }
}