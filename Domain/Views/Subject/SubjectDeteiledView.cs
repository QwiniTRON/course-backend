using System.Collections.Generic;
using Domain.Maps.Views;

namespace Domain.Views.Subject
{
    public class SubjectDeteiledView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public List<LessonView> Lessons { get; set; }

        public SubjectDeteiledView() {}
    }
}