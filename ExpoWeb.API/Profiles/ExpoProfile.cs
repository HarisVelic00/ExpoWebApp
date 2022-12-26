using AutoMapper;
using ExpoApp.Core.Models;
using ExpoApp.Service.ViewModels.Expo;

namespace ExpoWeb.API.Profiles
{
    public class ExpoProfile: Profile
    {
        public ExpoProfile()
        {
            CreateMap<Expo, ExpoVM>();
            CreateMap<ExpoCreationVM, Expo>();
            CreateMap<ExpoUpdateVM, Expo>().ForMember(expo => expo.Organiser, opt => opt.Ignore());
            CreateMap<ExpoUpdateVM, Expo>();
        }
    }
}
