using ExpoApp.Service.Shared;
using ExpoApp.Service.ViewModels.Tickets;
using System.Threading.Tasks;

namespace ExpoApp.Service.Interfaces
{
    public interface ITicketTypeService
    {
        Task<Response> GetTicketTypes();
        Task<Response> CreateTicketType(TicketTypeCreationVM ticketTypeCreaton);
        Task<Response> UpdateTicketType(TicketTypeUpdateVM ticketTypeCreaton);
        Task<Response> DeleteTicketType(int id);
    }
}
