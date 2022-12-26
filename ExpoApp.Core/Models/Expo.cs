using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpoApp.Core.Models
{
    public class Expo
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateOfOpening { get; set; }
        public DateTime DateOfClosing { get; set; }
        public string WorkHoursOpening { get; set; }
        public string WorkHoursClosing { get; set; }

        public Organiser Organiser { get; set; }
        public string OrganiserId { get; set; }
        public Location? Location { get; set; }
        public int? LocationId { get; set; }

        public Industry Industry { get; set; }
        public int IndustryId { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<TicketType> TicketTypes { get; set; }
    }
}
