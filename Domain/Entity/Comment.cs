using System;
using Domain.Abstractions;

namespace Domain.Entity
{
    public class Comment: IEntity
    {
        public int Id { get; set; }
        public string text { get; set; }
        public DateTime CreatedTime { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public Lesson Lesson { get; set; }
    }
}