using AutoMapper;
using Domain.Maps.Views;
using Domain.Maps.Views.Comment;

namespace Domain.Maps
{
    public class EntitiesView: Profile
    {
        public EntitiesView()
        {
            CreateMap<Entity.User, UserView>();
            CreateMap<Entity.Lesson, LessonView>();
            CreateMap<Entity.Lesson, LessonDetailedView>();
            CreateMap<Entity.Comment, CommentView>();
        }
    }
}