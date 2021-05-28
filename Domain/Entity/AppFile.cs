using System.Collections.Generic;
using Domain.Abstractions;

namespace Domain.Entity
{
    public class AppFile: IEntity
    {
        public AppFile(string name, string fullPath, string path)
        {
            Name = name;
            Path = path;
            FullPath = fullPath;
        }

        public int Id { get; set; }
        
        /* file name */
        public string Name { get; set; }
        /* full path */
        public string FullPath { get; set; }
        /* full path relative main catalog */
        public string Path { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        public List<Message> Messages { get; set; }
        public List<PracticeOrder> PracticeOrders { get; set; }
        
        protected AppFile(string fullPath)
        {
            FullPath = fullPath;
        }
    }
}