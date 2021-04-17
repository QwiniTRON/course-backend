using System.Collections.Generic;
using Domain.Abstractions;

namespace Domain.Entity
{
    public class AppFile: IEntity
    {
        public AppFile(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        public List<Message> Messages { get; set; }
        public List<PracticeOrder> PracticeOrders { get; set; }
        
        protected AppFile()
        {
            
        }
    }
}