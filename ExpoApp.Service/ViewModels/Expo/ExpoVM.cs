using ExpoApp.Core.Models;
using ExpoApp.Service.ViewModels.Industries;
using ExpoApp.Service.ViewModels.Location;
using ExpoApp.Service.ViewModels.Tickets;
using ExpoApp.Service.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpoApp.Service.ViewModels.Expo
{
    public class ExpoVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateOfOpening { get; set; }
        public DateTime DateOfClosing { get; set; }
        public string WorkHoursOpening { get; set; }
        public string WorkHoursClosing { get; set; }
        public bool HasExpired { get { return DateOfClosing < DateTime.Now;  } }
        public OrganizerVM Organiser { get; set; }
        public LocationVM Location { get; set; }
        public IndustryVM Industry { get; set; }
        public List<TicketVM> Tickets { get; set; }
        public List<TicketTypeVM> TicketTypes { get; set; }
    }
}
