using AutoMapper;
using Domain.Maps.Views;

namespace Domain.Maps
{
    public class LessonMapper: Profile
    {
        public LessonMapper()
        {
            CreateMap<Entity.Lesson, LessonView>();
            CreateMap<Entity.Lesson, LessonDetailedView>();
        }
    }
}