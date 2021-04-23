using AutoMapper;
using Domain.UseCases.PracticeOrder.GetMany;
using Domain.UseCases.PracticeOrder.GetOne;
using Domain.UseCases.PracticeOrder.OneInfo;
using Domain.UseCases.User.UserInfo;

namespace Domain.Maps
{
    public class PracticeOrderMappers: Profile
    {
        public PracticeOrderMappers()
        {
            /* Entity.PracticeOrder -> Get One Practice */
            CreateMap<Entity.PracticeOrder, GetOnePracticeOutput>()
                .ForMember("Author", 
                    x => x.MapFrom("Author"));

            /* Entity.PracticeOrder -> Get Many Practice */
            CreateMap<Entity.PracticeOrder, GetPracticesOutput>()
                .ForMember(
                    x => x.UserNick, x
                        => x.MapFrom(y => y.Author.Nick))
                .ForMember(
                    x => x.LessonName,
                    y => 
                        y.MapFrom(x => x.Lesson.Name));
            
            /* Entity.PracticeOrder -> Get One Practice Info */
            CreateMap<Entity.PracticeOrder, GetPracticeInfoOutput>()
                .ForMember("Author", x => 
                    x.MapFrom(y => y.Author))
                .ForMember("Lesson", x => 
                    x.MapFrom(y => y.Lesson))
                .ForMember("Teacher", x =>
                    x.MapFrom(y => y.Teacher))
                .ForMember("CodePath", x =>
                    x.MapFrom(y => y.PracticeContent.Path));
        }
    }
}