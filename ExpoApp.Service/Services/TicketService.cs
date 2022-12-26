using AutoMapper;
using ExpoApp.Core.Models;
using ExpoApp.Repository.Context;
using ExpoApp.Repository.Repostiory;
using ExpoApp.Service.Interfaces;
using ExpoApp.Service.Shared;
using ExpoApp.Service.ViewModels.Notification;
using ExpoApp.Service.ViewModels.Tickets;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpoApp.Service.Services
{
    public class TicketService: ITicketService
    {
        private readonly IRepository<Ticket> TicketRepository;
        private readonly IRepository<TicketType> TicketTypeRepository;
        private readonly IRepository<Expo> ExpoRepository;
        private readonly INotificationService NotificationService;
        private readonly UserManager<IdentityUser> UserManager;
        private readonly IMapper Mapper;

        public TicketService(IRepository<Ticket> ticketRepository,
            IMapper mapper,
            IRepository<Expo> expoRepository,
            IRepository<TicketType> ticketTypeRepository,
            UserManager<IdentityUser> userManager,
            INotificationService notificationService)
        {
            TicketRepository = ticketRepository;
            Mapper = mapper;
            ExpoRepository = expoRepository;
            TicketTypeRepository = ticketTypeRepository;
            UserManager = userManager;
            NotificationService = notificationService;
        }

        public IEnumerable<TicketVM> GetTickets()
        {
            var tickets = TicketRepository.GetAll();

            return Mapper.Map<IEnumerable<TicketVM>>(tickets.Result);
        }

        // TO DO: Remove method
        public async Task<Response> AddTicketType(TicketCreationVM ticketCreation)
        {
            var expoExist = await ExpoRepository.GetEntity(ticketCreation.ExpoId);

            if (expoExist == null)
            {
                return new Response()
                {
                    Message = "Ticket creation failed!",
                    IsSuccess = false,
                    Status = "error",
                    Errors = new List<string>() { "Expo doesn't exist!" }
                };
            }

            var typeExist = await TicketTypeRepository.GetEntity(ticketCreation.TicketTypeId);

            if (typeExist == null)
            {
                return new Response()
                {
                    Message = "Ticket creation failed!",
                    IsSuccess = false,
                    Status = "error",
                    Errors = new List<string>() { "Ticket type doesn't exist!" }
                };
            }

            var ticket = Mapper.Map<Ticket>(ticketCreation);

            ticket.DateTo = ticket.DateFrom.AddDays(typeExist.ValidDaysCount);

            var result = await TicketRepository.Add(ticket);

            var response = new Response<TicketVM>()
            {
                Message = "Ticket creation successfull!",
                IsSuccess = true,
                Status = "success",
                Data = Mapper.Map<TicketVM>(result)
            };

            return response;
        }

        public async Task<Response> ExpoTickets(int id)
        {
            var expoExist = await ExpoRepository.GetEntity(id);

            if (expoExist == null)
            {
                return new Response()
                {
                    Message = "Failed to get expo tickets!",
                    Status = "error",
                    IsSuccess = false,
                    Errors = new List<string>() { "Expo doesn't exist!" }
                };
            }

            var ticekts = await TicketRepository.GetAll();

            var expoTickets = ticekts.Where(ticket => ticket.ExpoId == id).ToList();

            return new Response<List<TicketVM>>()
            {
                Message = "Tickets recieved successfully!",
                IsSuccess = true,
                Status = "success",
                Data = Mapper.Map<List<TicketVM>>(expoTickets)
            };
        }

        public async Task<Response> DeleteTicket(int id)
        {
            var ticket = await TicketRepository.GetEntity(id);

            if (ticket is null)
            {
                return new Response()
                {
                    Message = "Failed to delete ticket!",
                    Status = "error",
                    IsSuccess = false,
                    Errors = new List<string>() { "Ticket doesn't exist!" }
                };
            }

            try
            {
                await TicketRepository.Delete(ticket);
            }
            catch (Exception exc)
            {
                return new Response()
                {
                    Message = "Failed to delete ticekt!",
                    Status = "error",
                    IsSuccess = false,
                    Errors = new List<string>() { exc.InnerException.Message }
                };
            }

            return new Response()
            {
                Message = "Ticket deleted successfully",
                Status = "success",
                IsSuccess = true
            };
        }

        //public async Task<Response> EditTicket(int id, TicketCreationVM ticketUpdateCreation)
        //{
        //    var ticketExist = await TicketRepository.GetEntity(id);

        //    if (ticketExist == null)
        //    {
        //        return new Response()
        //        {
        //            Message = "Ticket update failed!",
        //            IsSuccess = false,
        //            Status = "error",
        //            Errors = new List<string>() { "Ticket doesn't exist!" }
        //        };
        //    }

        //    ticketExist = Mapper.Map<Ticket>(ticketUpdateCreation);
        //    ticketExist.DateTo = ticketExist.DateFrom.AddDays(ticketUpdateCreation.ValidDaysCount);

        //    await ExpoContext.SaveChangesAsync();

        //    return new Response<TicketVM>()
        //    {
        //        Message = "Ticket update successfull!",
        //        IsSuccess = true,
        //        Status = "success",
        //        Data = Mapper.Map<TicketVM>(ticketExist)
        //    };
        //}

        public async Task<Response> PurcahseTicket(TicketPurchaseVM ticketPurchase)
        {
            var expo = await ExpoRepository.GetEntity(ticketPurchase.ExpoId);

            if (expo is null)
            {
                return new Response()
                {
                    Message = "Failed to purchase ticket!",
                    Status = "error",
                    IsSuccess = false,
                    Errors = new List<string>() { "Expo doesn't exist" }
                };
            }

            if (ticketPurchase.DateFrom > expo.DateOfClosing || ticketPurchase.DateFrom < DateTime.Now)
            {
                return new Response()
                {
                    Message = "Failed to purchase ticket!",
                    Status = "error",
                    IsSuccess = false,
                    Errors = new List<string>() { "Invalid dates!" }
                };
            }

            var type = await TicketTypeRepository.GetEntity(ticketPurchase.TypeId);

            if (type is null)
            {
                return new Response()
                {
                    Message = "Failed to purchase ticket!",
                    Status = "error",
                    IsSuccess = false,
                    Errors = new List<string>() { "Invalid ticket type!" }
                };
            }

            var user = await UserManager.FindByNameAsync(ticketPurchase.Username);

            if (user is null)
            {
                return new Response()
                {
                    Message = "Failed to purchase ticket!",
                    Status = "error",
                    IsSuccess = false,
                    Errors = new List<string>() { "User doesn't exist!" }
                };
            }

            var ticket = Mapper.Map<Ticket>(ticketPurchase);
            ticket.DateTo = ticketPurchase.DateFrom.AddDays(type.ValidDaysCount);
            ticket.UserId = user.Id;
            ticket.Price = type.Price;

            try
            {
                var result = await TicketRepository.Add(ticket);

                var notification = new NotificationCreationVM()
                {
                    Title = "Purchased ticket!",
                    Content = $"{user.UserName} has purchased {type.Name} ticket!",
                    UserId = expo.OrganiserId,
                   CreationDate = DateTime.Now
                };

                await NotificationService.AddNotifications(notification);

                return new Response<TicketVM>()
                {
                    Message = "Successfully purchased ticket!",
                    Status = "success",
                    IsSuccess = true,
                    Data = Mapper.Map<TicketVM>(result)
                };
            }
            catch (Exception exc)
            {
                return new Response()
                {
                    Message = "Failed to purchase ticket!",
                    Status = "error",
                    IsSuccess = false,
                    Errors = new List<string>() { exc.Message, exc.InnerException?.Message }
                };
            }
        }
    }
}
