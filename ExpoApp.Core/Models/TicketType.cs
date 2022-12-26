using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpoApp.Core.Models
{
    public class TicketType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int ValidDaysCount { get; set; }
        public double Price { get; set; }
        public Expo Expo { get; set; }
        public int ExpoId { get; set; }
    }
}
