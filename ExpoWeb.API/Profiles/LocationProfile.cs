using AutoMapper;
using ExpoApp.Core.Models;
using ExpoApp.Service.ViewModels.Location;

namespace ExpoWeb.API.Profiles
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<Location, LocationVM>();
            CreateMap<LocationCreationVM, Location>();
        }
    }
}
