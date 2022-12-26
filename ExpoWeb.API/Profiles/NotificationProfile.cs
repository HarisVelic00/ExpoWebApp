using AutoMapper;
using ExpoApp.Core.Models;
using ExpoApp.Service.ViewModels.Notification;

namespace ExpoWeb.API.Profiles
{
    public class NotificationProfile: Profile
    {
        public NotificationProfile()
        {
            CreateMap<NotificationCreationVM, Notification>();
            CreateMap<Notification, NotificationVM>();
        }
    }
}
