using System;

namespace Domain.Views.UserProgress
{
    public class UserProgressView
    {
        public int Id { get; set; }
                
        public int UserId { get; set; }

        public int LessonId { get; set; }

        public DateTime CreatedTime { get; set; }
        
        public UserProgressView() {}
    }
}