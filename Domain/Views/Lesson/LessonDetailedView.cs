using System.Collections.Generic;
using Domain.Maps.Views.Comment;
using Domain.Views.Subject;

namespace Domain.Maps.Views
{
    public class LessonDetailedView
    {
        public int Id { get; set; }
        public SubjectView Subject { get; set; }
        public string Name { get; set; }
        public bool IsPractice { get; set; }
        public int Index { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
    
        public List<CommentView> Comments { get; set; }

        public LessonDetailedView() {}
    }
}