using System.Collections.Generic;
using Domain.Maps.Views.Comment;

namespace Domain.Maps.Views
{
    public class LessonDetailedView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPractice { get; set; }
    
        public List<CommentView> Comments { get; set; }
        
        public LessonDetailedView() {}
    }
}