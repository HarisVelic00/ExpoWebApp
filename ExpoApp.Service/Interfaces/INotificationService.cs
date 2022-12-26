using ExpoApp.Service.Shared;
using ExpoApp.Service.ViewModels.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpoApp.Service.Interfaces
{
    public interface INotificationService
    {
        Task<List<NotificationVM>> GetNotifications(string id);
        Task AddNotifications(NotificationCreationVM notificationCreation);
        Task<int> GetNotificationsCount(string username);
        Task MarkNotificationAsSeen(int id);
    }
}
