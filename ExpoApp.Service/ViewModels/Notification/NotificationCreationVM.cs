using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpoApp.Service.ViewModels.Notification
{
    public class NotificationCreationVM
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
