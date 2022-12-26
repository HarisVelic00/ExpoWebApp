using ExpoApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpoApp.Service.ViewModels.Tickets
{
    public class TicketPurchaseVM
    {
        public int TypeId { get; set; }
        public int ExpoId { get; set; }
        public DateTime DateFrom { get; set; }
        public string Username { get; set; }
    }
}
