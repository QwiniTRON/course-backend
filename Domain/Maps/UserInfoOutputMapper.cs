using System.Collections.Generic;
using AutoMapper;
using Domain.Enums;
using Domain.UseCases.User.UserInfo;

namespace Domain.Maps
{
    public class UserInfoOutputMapper: Profile
    {
        public UserInfoOutputMapper()
        {
            CreateMap<Entity.User, UserInfoOutput>();
        }
    }
}