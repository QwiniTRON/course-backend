using AutoMapper;
using Domain.UseCases.PracticeOrder.GetMany;
using Domain.UseCases.PracticeOrder.GetOne;
using Domain.UseCases.User.UserInfo;

namespace Domain.Maps
{
    public class PracticeOrderMappers: Profile
    {
        public PracticeOrderMappers()
        {
            CreateMap<Entity.PracticeOrder, GetOnePracticeOutput>();

            CreateMap<Entity.PracticeOrder, GetPracticesOutput>()
                .ForMember(
                    x => x.UserNick, x
                        => x.MapFrom(y => y.Author.Nick))
                .ForMember(
                    x => x.LessonName,
                    y => 
                        y.MapFrom(x => x.Lesson.Name));
        }
    }
}