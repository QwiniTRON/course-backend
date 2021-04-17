using System.Collections.Generic;
using Domain.Abstractions;

namespace Domain.Entity
{
    public class Chat: IEntity
    {
        public int Id { get; set; }

        public List<User> Members { get; set; }

        public List<Message> Messages { get; set; }
        
        
        protected Chat()
        {
            
        }
    }
}