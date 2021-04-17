using System;
using Domain.Abstractions;

namespace Domain.Entity
{
    public class Comment: IEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedTime { get; set; }

        public User Author { get; set; }
        public int AuthorId { get; set; }

        public Lesson Lesson { get; set; }

        protected Comment()
        {
        }
    }
}