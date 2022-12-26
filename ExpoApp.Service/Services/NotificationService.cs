using AutoMapper;
using ExpoApp.Core.Models;
using ExpoApp.Repository.Repostiory;
using ExpoApp.Service.Interfaces;
using ExpoApp.Service.ViewModels.Notification;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpoApp.Service.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IRepository<Notification> NotificationRepositroy;
        private readonly IMapper Mapper;
        private readonly IHubContext<BroadcastHub, IHubClient> HubContext;
        private readonly UserManager<IdentityUser> UserManager;

        public NotificationService(IRepository<Notification> notificationRepositroy,
            IMapper mapper,
            IHubContext<BroadcastHub, IHubClient> hubContext,
            UserManager<IdentityUser> userManager)
        {
            NotificationRepositroy = notificationRepositroy;
            Mapper = mapper;
            HubContext = hubContext;
            UserManager = userManager;
        }

        public async Task AddNotifications(NotificationCreationVM notificationCreation)
        {
            var notification = Mapper.Map<Notification>(notificationCreation);

            await NotificationRepositroy.Add(notification);

            await HubContext.Clients.All.BroadCastMessage();
        }

        public async Task<List<NotificationVM>> GetNotifications(string username)
        {
            var user = await UserManager.FindByNameAsync(username);

            var notifications = await NotificationRepositroy.GetAll();

            var data = notifications.Where(notification => notification.UserId == user.Id).OrderByDescending(notification => notification.CreationDate).ToList();

            return Mapper.Map<List<NotificationVM>>(data);
        }

        public async Task<int> GetNotificationsCount(string username)
        {
            var user = await UserManager.FindByNameAsync(username);

            var notifications = await NotificationRepositroy.GetAll();

            var data = notifications.Where(notification => notification.UserId == user.Id && !notification.IsSeen).ToList();

            return data.Count;
        }

        public async Task MarkNotificationAsSeen(int id)
        {
            var notification = await NotificationRepositroy.GetEntity(id);

            if (notification != null)
            {
                notification.IsSeen = true;

                await NotificationRepositroy.Update(notification);

                await HubContext.Clients.All.BroadCastMessage();
            }
        }
    }
}
