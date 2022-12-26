using ExpoApp.Core.Models;
using ExpoApp.Service.Shared;
using ExpoApp.Service.ViewModels.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpoApp.Service.Interfaces
{
    public interface ITicketService
    {
        IEnumerable<TicketVM> GetTickets();
        Task<Response> AddTicketType(TicketCreationVM karta);
        // Task<Response> EditTicket(int id, TicketCreationVM ticket);
        Task<Response> ExpoTickets(int id);
        Task<Response> DeleteTicket(int id);
        Task<Response> PurcahseTicket(TicketPurchaseVM ticketPurchase);
    }
}