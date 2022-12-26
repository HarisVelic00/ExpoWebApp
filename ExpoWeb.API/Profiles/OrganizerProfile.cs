using AutoMapper;
using ExpoApp.Core.Models;
using ExpoApp.Service.ViewModels.User;

namespace ExpoWeb.API.Profiles
{
    public class OrganizerProfile : Profile
    {
        public OrganizerProfile()
        {
            CreateMap<Organiser, OrganizerVM>();
        }
    }
}
