using System.Collections.Generic;
using Domain.Maps.Views.Comment;
using Domain.Views.Subject;

namespace Domain.Maps.Views
{
    public class LessonDetailedView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPractice { get; set; }
        public SubjectView Subject { get; set; }
    
        public List<CommentView> Comments { get; set; }

        public LessonDetailedView() {}
    }
}