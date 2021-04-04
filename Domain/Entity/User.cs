using Domain.Abstractions;

namespace Domain.Entity
{
    public class User: IEntity
    {
        public User(string mail, string nick, string password)
        {
            Mail = mail;
            Nick = nick;
            Password = password;

            IsPassed = false;
            IsBanned = false;
        }

        public int Id { get; set; }
        public string Mail { get; set; }
        public string Nick { get; set; }
        public string Password { get; set; }
        // add role
        public bool IsPassed { get; set; } 
        public bool IsBanned { get; set; }
        
        protected User() { }
    }
}