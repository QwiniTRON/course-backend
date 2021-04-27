using AutoMapper;
using Domain.Entity;
using Domain.Views.SubjectSertificate;

namespace Domain.Maps
{
    public class SubjectSertificateMapper: Profile
    {
        public SubjectSertificateMapper()
        {
            CreateMap<SubjectSertificate, SubjectSertificateView>();
        }
    }
}