using AutoMapper;
using Domain.Entity;
using Domain.UseCases.Progress.ProgressesById;

namespace Domain.Maps
{
    public class UserProgressesMapper: Profile
    {
        public UserProgressesMapper()
        {
            CreateMap<UserProgress, ProgressesByIdOutput>();
        }
    }
}