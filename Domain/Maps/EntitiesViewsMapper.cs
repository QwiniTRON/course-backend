using AutoMapper;
using Domain.Maps.EntitiesViews;

namespace Domain.Maps
{
    public class EntitiesView: Profile
    {
        public EntitiesView()
        {
            CreateMap<Entity.User, UserView>();
            CreateMap<Entity.Lesson, LessonView>();
        }
    }
}