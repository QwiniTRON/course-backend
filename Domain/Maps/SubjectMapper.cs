using AutoMapper;
using Domain.Entity;
using Domain.Views.Subject;

namespace Domain.Maps
{
    public class SubjectMapper: Profile
    {
        public SubjectMapper()
        {
            CreateMap<Subject, SubjectView>();
            CreateMap<Subject, SubjectDeteiledView>();
        }
    }
}