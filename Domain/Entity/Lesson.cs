using System.Collections.Generic;
using Domain.Abstractions;

namespace Domain.Entity
{
    public class Lesson: IEntity
    {
        public Lesson(string name)
        {
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public List<UserProgress> UserProgresses { get; set; }
        public List<Comment> Comments { get; set; }
        public List<PracticeOrder> PracticeOrders { get; set; }

        protected Lesson()
        {
            
        }
    }
}