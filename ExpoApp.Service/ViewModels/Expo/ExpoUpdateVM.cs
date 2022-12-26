using ExpoApp.Service.ViewModels.Location;
using ExpoApp.Service.ViewModels.Tickets;
using System;
using System.Collections.Generic;

namespace ExpoApp.Service.ViewModels.Expo
{
    public class ExpoUpdateVM
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime DateOfOpening { get; set; }
        public DateTime DateOfClosing { get; set; }
        public string? WorkHoursOpening { get; set; }
        public string? WorkHoursClosing { get; set; }
        public string Organizer { get; set; }
        public int IndustryId { get; set; }
        //public LocationVM Location { get; set; }
        //public List<TicketCreationVM> Ticekts { get; set; }
    }
}
