using System;
using Domain.Abstractions;

namespace Domain.Entity
{
    public class SubjectSertificate: IEntity
    {
        public SubjectSertificate(User owner, Subject subject)
        {
            Owner = owner;
            Subject = subject;
        }

        public int Id { get; set; }

        public User Owner { get; set; }
        public int OwnerId { get; set; }
        
        public Subject Subject { get; set; }
        public int SubjectId { get; set; }
        
        public DateTime CreatedTime { get; set; }
        
        protected SubjectSertificate() {}
    }
}