using System.Collections.Generic;
using AutoMapper;
using Domain.Abstractions.Mediatr;
using Domain.Enums;

namespace Domain.UseCases.User.UserInfo
{
    public class UserInfoOutput: IUseCaseOutput
    {
        public int Id { get; set; }
        public string Nick { get; set; }
        public List<UserRoles> Roles { get; set; }
        public string Mail { get; set; }
        public bool IsBanned { get; set; }
    }
}