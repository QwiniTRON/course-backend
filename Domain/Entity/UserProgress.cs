using Domain.Abstractions;

namespace Domain.Entity
{
    public class UserProgress: IEntity
    {
        public UserProgress(User user, Lesson lesson)
        {
            User = user;
            Lesson = lesson;
        }

        public int Id { get; set; }
        
        public User User { get; set; }
        public int UserId { get; set; }

        public Lesson Lesson { get; set; }
        public int LessonId { get; set; }

        protected UserProgress()
        {
            
        }
    }
}