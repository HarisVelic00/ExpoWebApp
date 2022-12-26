using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpoApp.Core.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public TicketType Type { get; set; }
        public int TypeId { get; set; }
        public Expo Expo { get; set; }
        public int ExpoId { get; set; }
        public IdentityUser User { get; set; }
        public string UserId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public double Price { get; set; }
    }
}
