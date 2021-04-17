using AutoMapper;
using Domain.Abstractions.Mediatr;
using Domain.Enums;

namespace Domain.UseCases.User.UserInfo
{
    public class UserInfoOutput: IUseCaseOutput
    {
        public int Id { get; set; }
        public string Nick { get; set; }
        public UserRoles Role { get; set; }
        public string Mail { get; set; }
        public bool IsBanned { get; set; }
    }

    public class SomeMapper : Profile
    {
        public SomeMapper()
        {
            CreateMap<Entity.User, UserInfoOutput>();

        }
    }
}