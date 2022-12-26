using AutoMapper;
using ExpoApp.Core.Models;
using ExpoApp.Service.ViewModels.Industries;

namespace ExpoWeb.API.Profiles
{
    public class IndustryProfile : Profile
    {
        public IndustryProfile()
        {
            CreateMap<Industry, IndustryVM>();
            CreateMap<IndustryCreationVM, Industry>();
        }
    }
}
