using System.Collections.Generic;
using Domain.Abstractions;

namespace Domain.Entity
{
    public class Lesson: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<UserProgress> UserProgresses { get; set; }
    }
}