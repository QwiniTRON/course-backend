using AutoMapper;
using Domain.Maps.Views;
using Domain.Maps.Views.Comment;

namespace Domain.Maps
{
    public class EntitiesView: Profile
    {
        public EntitiesView()
        {
            /* user */
            CreateMap<Entity.User, UserView>();
            CreateMap<Entity.User, UserViewDetailed>();
            
            /* lesson */
            CreateMap<Entity.Lesson, LessonView>();
            CreateMap<Entity.Lesson, LessonDetailedView>();
            
            /* comment */
            CreateMap<Entity.Comment, CommentView>();
        }
    }
}