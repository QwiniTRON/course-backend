using System.Collections.Generic;
using Domain.Abstractions;

namespace Domain.Entity
{
    public class Lesson: IEntity
    {
        public Lesson(string name, bool isPractice, Subject subject, int index, string description, string content)
        {
            Name = name;
            IsPractice = isPractice;
            Subject = subject;
            Index = index;
            Description = description;
            Content = content;
        }

        public int Id { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public bool IsPractice { get; set; }
        
        public Subject Subject { get; set; }
        public int SubjectId { get; set; }

        public List<UserProgress> UserProgresses { get; set; }
        public List<Comment> Comments { get; set; }
        public List<PracticeOrder> PracticeOrders { get; set; }

        protected Lesson(int index, string description)
        {
            Index = index;
            Description = description;
        }
    }
}