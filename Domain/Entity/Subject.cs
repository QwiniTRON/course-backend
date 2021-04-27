using System.Collections.Generic;
using Domain.Abstractions;

namespace Domain.Entity
{
    public class Subject: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<SubjectSertificate> SubjectSertificates { get; set; }
        public List<Lesson> Lessons { get; set; }
        
        protected Subject() {}

        public Subject(string name)
        {
            Name = name;
        }
    }
}