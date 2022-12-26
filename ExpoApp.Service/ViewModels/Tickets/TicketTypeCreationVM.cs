using ExpoApp.Core.Models;

namespace ExpoApp.Service.ViewModels.Tickets
{
    public class TicketTypeCreationVM
    {
        public string Name { get; set; }
        public int ValidDaysCount { get; set; }
        public double Price { get; set; }
        public int ExpoId { get; set; }
    }
}