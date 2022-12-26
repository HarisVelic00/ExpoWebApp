using ExpoApp.Core.Models;
using ExpoApp.Service.ViewModels.Expo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpoApp.Service.ViewModels.Tickets
{
    public class TicketVM
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public int ExpoId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public double Price { get; set; }
    }
}
