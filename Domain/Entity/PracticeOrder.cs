using System;
using Domain.Abstractions;

namespace Domain.Entity
{
    public class PracticeOrder: IEntity
    {
        public int Id { get; set; }

        public User Author { get; set; }
        public int AuthorId { get; set; }

        
        public string RejectReason { get; set; }
        public bool IsDone { get; set; }
        public bool IsResolved { get; set; }

        
        public Lesson Lesson { get; set; }
        public int LessonId { get; set; }

        
        public User Teacher { get; set; }
        public int TeacherId { get; set; }

        public AppFile PracticeContent { get; set; }
        public int PracticeContentId { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        
        /* ctors */
        public PracticeOrder(User author, Lesson lesson, AppFile practiceContent)
        {
            Author = author;
            Lesson = lesson;
            PracticeContent = practiceContent;
            
            IsDone = false;
        }
        protected PracticeOrder(){}
    }
}