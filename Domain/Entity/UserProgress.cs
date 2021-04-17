using Domain.Abstractions;

namespace Domain.Entity
{
    public class UserProgress: IEntity
    {
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