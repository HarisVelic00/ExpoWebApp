using AutoMapper;
using ExpoApp.Service.ViewModels.User;
using Microsoft.AspNetCore.Identity;

namespace ExpoWeb.API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile(){
        CreateMap<IdentityUser, UserVM>();
        }
    }
}
