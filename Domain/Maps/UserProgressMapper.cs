using AutoMapper;
using Domain.Entity;
using Domain.Views.UserProgress;

namespace Domain.Maps
{
    public class UserProgressMapper: Profile
    {
        public UserProgressMapper()
        {
            CreateMap<UserProgress, UserProgressView>();
        }
        
    }
}