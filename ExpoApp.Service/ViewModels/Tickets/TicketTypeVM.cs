using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpoApp.Service.ViewModels.Tickets
{
    public class TicketTypeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ValidDaysCount { get; set; }
        public double Price { get; set; }
        public int ExpoId { get; set; }
    }
}
