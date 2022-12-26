using AutoMapper;
using ExpoApp.Core.Models;
using ExpoApp.Service.Interfaces;
using ExpoApp.Service.ViewModels.Tickets;

namespace ExpoWeb.API.Profiles
{
    public class TicketProfile: Profile
    {
        public TicketProfile()
        {
            CreateMap<TicketCreationVM, Ticket>();
            CreateMap<Ticket, TicketVM>();

            CreateMap<TicketType, TicketTypeVM>();
            CreateMap<TicketTypeCreationVM, TicketType>();
            CreateMap<TicketTypeUpdateVM, TicketType>();

            CreateMap<TicketCreationVM, TicketType>();
            CreateMap<TicketPurchaseVM, Ticket>();
        }
    }
}
