using AutoMapper;
using Domain.Entity;

namespace Domain.Maps
{
    public class AppFileView: Profile
    {
        public AppFileView()
        {
            CreateMap<AppFile, Domain.Views.AppFile.AppFileView>();
        }
    }
}