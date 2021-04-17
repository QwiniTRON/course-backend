using System;
using Domain.Abstractions;

namespace Domain.Entity
{
    public class Message: IEntity
    {
        public int Id { get; set; }
        
        public string Text { get; set; }
        public AppFile Content { get; set; }
        public int ContentId { get; set; }
        
        public User Author { get; set; }
        public int AuthorId { get; set; }

        public Chat Chat { get; set; }
        public int ChatId { get; set; }

        public DateTime CreatedTime { get; set; }

        protected Message(){}
    }
}