using AutoMapper;
using ExpoApp.Core.Models;
using ExpoApp.Repository.Repostiory;
using ExpoApp.Service.Interfaces;
using ExpoApp.Service.Shared;
using ExpoApp.Service.ViewModels.Tickets;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ExpoApp.Service.Services
{
    public class TicketTypeService : ITicketTypeService
    {
        private readonly IRepository<TicketType> TicketTypeRepository;
        private readonly IRepository<Expo> ExpoRepository;
        private readonly IMapper Mapper;

        public TicketTypeService(IRepository<TicketType> ticketTypeRepository, IRepository<Expo> expoRepository, IMapper mapper)
        {
            TicketTypeRepository = ticketTypeRepository;
            ExpoRepository = expoRepository;
            Mapper = mapper;
        }

        public async Task<Response> CreateTicketType(TicketTypeCreationVM ticketTypeCreation)
        {
            var expoExist = await ExpoRepository.GetEntity(ticketTypeCreation.ExpoId);

            if (expoExist is null)
            {
                return new Response()
                {
                    Message = "Failed to create ticket type!",
                    Status = "error",
                    IsSuccess = false,
                    Errors = new List<string>() { "Expo doesn't exist!" }
                };
            }

            var ticketType = Mapper.Map<TicketType>(ticketTypeCreation);

            try
            {
                var result = await TicketTypeRepository.Add(ticketType);
            
                return new Response<TicketTypeVM>()
                {
                    Message = "Ticket type created successfully!",
                    Status = "success",
                    IsSuccess = true,
                    Data = Mapper.Map<TicketTypeVM>(result)
                };
            }
            catch (Exception exc)
            {
                return new Response()
                {
                    Message = "Faield to create ticket type!",
                    Status = "error",
                    IsSuccess = false,
                    Errors = new List<string>() { exc.InnerException?.Message }
                };
            }
        }

        public async Task<Response> DeleteTicketType(int id)
        {

            var ticketExist = await TicketTypeRepository.GetEntity(id);

            if (ticketExist is null)
            {
                return new Response()
                {
                    Message = "Failed to delete ticket type!",
                    Status = "error",
                    IsSuccess = false,
                    Errors = new List<string>() { "Ticket type doesn't exist!" }
                };
            }

            try
            {
                await TicketTypeRepository.Delete(ticketExist);

                return new Response()
                {
                    Message = "Ticket type deleted successfully!",
                    Status = "success",
                    IsSuccess = true,
                };
            }
            catch (Exception exc)
            {
                return new Response()
                {
                    Message = "Faield to create ticket type!",
                    Status = "error",
                    IsSuccess = false,
                    Errors = new List<string>() { exc.InnerException?.Message }
                };
            }
        }

        public Task<Response> GetTicketTypes()
        {
            throw new NotImplementedException();
        }

        public async Task<Response> UpdateTicketType(TicketTypeUpdateVM ticketTypeCreation)
        {
            var ticketExist = await TicketTypeRepository.GetEntity(ticketTypeCreation.Id);

            if (ticketExist is null)
            {
                return new Response()
                {
                    Message = "Failed to update ticket type!",
                    Status = "error",
                    IsSuccess = false,
                    Errors = new List<string>() { "Ticket type doesn't exist!" }
                };
            }

            //ticketExist = Mapper.Map<TicketType>(ticketTypeCreation);
            ticketExist.ExpoId = ticketTypeCreation.ExpoId;
            ticketExist.Price = ticketTypeCreation.Price;
            ticketExist.Name = ticketTypeCreation.Name;
            ticketExist.ValidDaysCount = ticketTypeCreation.ValidDaysCount;

            try
            {
                var result = await TicketTypeRepository.Update(ticketExist);

                return new Response<TicketTypeVM>()
                {
                    Message = "Ticket type created successfully!",
                    Status = "success",
                    IsSuccess = true,
                    Data = Mapper.Map<TicketTypeVM>(result)
                };
            }
            catch (Exception exc)
            {
                return new Response()
                {
                    Message = "Faield to create ticket type!",
                    Status = "error",
                    IsSuccess = false,
                    Errors = new List<string>() { exc.InnerException?.Message }
                };
            }
        }
    }
}
