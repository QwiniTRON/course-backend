using System;
using System.Collections.Generic;
using Domain.Abstractions;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entity
{
    public class User: IEntity
    {
        public User(string mail, string nick, string password, UserRoles role = UserRoles.Participant)
        {
            Mail = mail;
            Nick = nick;
            Password = password;
            Role = role;
            
            IsBanned = false;
        }

        public int Id { get; set; }
        public string Mail { get; set; }
        public string Nick { get; set; }
        public string Password { get; set; }
        // add role
        public bool IsBanned { get; set; }
        public UserRoles Role { get; set; }


        public List<UserProgress> UserProgresses { get; set; }
        
        protected User() { }
    }
}