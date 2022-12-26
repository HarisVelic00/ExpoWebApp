using ExpoApp.Service.Interfaces;
using ExpoApp.Service.ViewModels.Notification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpoWeb.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService NotificationService;
        public NotificationController(INotificationService notificationService)
        {
            NotificationService = notificationService;
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<List<NotificationVM>>> GetUserNotifications(string username)
        {
            var response = await NotificationService.GetNotifications(username);

            return Ok(response);
        }
        
        [HttpGet("{username}")]
        public async Task<ActionResult<List<NotificationVM>>> GetNotificationsCount(string username)
        {
            var response = await NotificationService.GetNotificationsCount(username);

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> SetNotificationAsSeen(int id)
        {
            await NotificationService.MarkNotificationAsSeen(id);
            return NoContent();
        }
    }
}
