using AutoMapper;
using ExpoApp.Core.Models;
using ExpoApp.Repository.Context;
using ExpoApp.Repository.Repostiory;
using ExpoApp.Service.Interfaces;
using ExpoApp.Service.SearchModels;
using ExpoApp.Service.Shared;
using ExpoApp.Service.ViewModels.Expo;
using ExpoApp.Service.ViewModels.Industries;
using ExpoApp.Service.ViewModels.Location;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Owin.Security.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpoApp.Service.Services
{
    public class ExpoService : IExpoService
    {
        private readonly IRepository<Expo> ExpoRepository;
        private readonly IRepository<Industry> IndustryRepository;
        private readonly IRepository<Location> LocationRepository;
        private readonly IRepository<TicketType> TicketTypeRepository;
        private readonly UserManager<IdentityUser> UserManager;
        private readonly IMapper Mapper;
        private readonly ExpoContext ExpoContext;

        public ExpoService(IRepository<Expo> expoRepository, UserManager<IdentityUser> userManager, IMapper mapper, ExpoContext expoContext, IRepository<Industry> industryRepository, IRepository<Location> locationRepository, IRepository<TicketType> ticketTypeRepository)
        {
            UserManager = userManager;
            ExpoRepository = expoRepository;
            Mapper = mapper;
            ExpoContext = expoContext;
            IndustryRepository = industryRepository;
            LocationRepository = locationRepository;
            TicketTypeRepository = ticketTypeRepository;
        }

        public async Task<Response> AddExpoAsync(ExpoCreationVM expoCreation)
        {
            var organizer = await UserManager.FindByNameAsync(expoCreation.Organizer);

            if (organizer == null)
            {
                return new Response()
                {
                    IsSuccess = false,
                    Message = "Expo creation failed!",
                    Status = "error",
                    Errors = new List<string>() { "User doesn't exist!"}
                };
            }

            if (string.IsNullOrWhiteSpace(expoCreation.Title) || expoCreation.DateOfOpening < DateTime.Now || expoCreation.DateOfClosing < expoCreation.DateOfOpening)
            {
                return new Response()
                {
                    IsSuccess = false,
                    Message = "Expo creation failed!",
                    Status = "error",
                    Errors = new List<string>() { "Invalid data provided!" }
                };
            }

            var expo = Mapper.Map<Expo>(expoCreation);
            expo.OrganiserId = organizer.Id;

            var result = await ExpoRepository.Add(expo);

            var response = new Response<ExpoVM>()
            {
                IsSuccess = true,
                Message = "Expo successfully added!",
                Status = "success",
                Data = Mapper.Map<ExpoVM>(result)
            };

            return response;
        }
        public async Task<Response> GetExpos(ExpoSearchModel expoSearch = null)
        {
            var data = ExpoContext.Expos
                .Include(expo => expo.Industry)
                .Include(expo => expo.Organiser)
                .Include(expo => expo.Location)
                .OrderBy(expo => expo.DateOfOpening)
                .AsEnumerable();

            data = await FilterData(data, expoSearch);

            if (expoSearch?.Page.HasValue == true && expoSearch?.PageSize.HasValue == true)
            {
                data = data.Take(expoSearch.PageSize.Value).Skip(expoSearch.Page.Value * expoSearch.PageSize.Value);
            }

            return new Response<IEnumerable<ExpoVM>>()
            {
                Message = "Expos retrieved!",
                Status = "success",
                IsSuccess = true,
                Data = Mapper.Map<IEnumerable<ExpoVM>>(data)
            };
        }
        public async Task<Response> UpdateExpoAsync(int id, ExpoUpdateVM input)
        {
            var organiser = await UserManager.FindByNameAsync(input.Organizer);

            if (organiser is null)
            {
                return new Response()
                {
                    IsSuccess = false,
                    Message = "Expo update failed!",
                    Status = "error",
                    Errors = new List<string>() { "User doesn't exist!" }
                };
            }


            if (string.IsNullOrWhiteSpace(input.Title) || input.DateOfOpening < DateTime.Now || input.DateOfClosing < input.DateOfOpening)
            {
                return new Response()
                {
                    IsSuccess = false,
                    Message = "Expo creation failed!",
                    Status = "error",
                    Errors = new List<string>() { "Invalid data provided!" }
                };
            }

            var expo = await ExpoRepository.GetEntity(id);

            if (expo is null)
            {
                return new Response()
                {
                    IsSuccess = false,
                    Message = "Expo update failed!",
                    Status = "error",
                    Errors = new List<string>() { "Expo doesn't exist!" }
                };
            }

            if (!isOrganiser(organiser).Result || !canEditExpo(organiser, expo))
            {
                return new Response()
                {
                    IsSuccess = false,
                    Message = "Expo update failed!",
                    Status = "error",
                    Errors = new List<string>() { "Only organizers can edit expos!" }
                };
            }

            expo.Title = input.Title;
            expo.Description = input.Description;
            expo.DateOfOpening = input.DateOfOpening;
            expo.DateOfClosing = input.DateOfClosing;
            expo.WorkHoursOpening = input.WorkHoursOpening;
            expo.WorkHoursClosing = input.WorkHoursClosing;
            expo.IndustryId = input.IndustryId;

            var result = await ExpoRepository.Update(expo);

            var response = new Response<ExpoVM>()
            {
                IsSuccess = true,
                Message = "Expo successfully updated!",
                Status = "success",
                Data = Mapper.Map<ExpoVM>(result)
            };

            return response;
        }
        public async Task<Response> DeleteExpoAsync(int id, string username)
        {
            var expoExist = await ExpoRepository.GetEntity(id);

            if (expoExist == null)
            {
                return new Response()
                {
                    IsSuccess = false,
                    Message = "Failed to delete expo!",
                    Status = "error",
                    Errors = new List<string>() { "Expo doesn't exist!" }
                };
            }

            var organiser = await UserManager.FindByNameAsync(username);

            if (expoExist.OrganiserId != organiser.Id)
            {
                return new Response()
                {
                    IsSuccess = false,
                    Message = "Failed to delete expo!",
                    Status = "error",
                    Errors = new List<string>() { "You don't have permision to delete expo!" }
                };
            }

            try
            {
                await ExpoRepository.Delete(expoExist);
            }
            catch (Exception exc)
            {
                return new Response()
                {
                    IsSuccess = false,
                    Message = "Failed to delete expo!",
                    Status = "error",
                    Errors = new List<string>() {exc.InnerException?.Message }
                };
            }

            return new Response()
            {
                IsSuccess = true,
                Message = "Expo deleted successfully!",
                Status = "success"
            };
        }

        public async Task<Response> AdminDeleteExpo(int id)
        {
            var expoExist = await ExpoRepository.GetEntity(id);

            if (expoExist == null)
            {
                return new Response()
                {
                    IsSuccess = false,
                    Message = "Failed to delete expo!",
                    Status = "error",
                    Errors = new List<string>() { "Expo doesn't exist!" }
                };
            }

            try
            {
                await ExpoRepository.Delete(expoExist);
            }
            catch (Exception exc)
            {
                return new Response()
                {
                    IsSuccess = false,
                    Message = "Failed to delete expo!",
                    Status = "error",
                    Errors = new List<string>() { exc.InnerException?.Message }
                };
            }

            return new Response()
            {
                IsSuccess = true,
                Message = "Expo deleted successfully!",
                Status = "success"
            };
        }

        public async Task<Response> UserExpos(string username)
        {
            var user = await UserManager.FindByNameAsync(username);

            if (user == null)
            {
                return new Response()
                {
                    IsSuccess = false,
                    Message = "Failed to retrieve expos!",
                    Status = "error",
                    Errors = new List<string>() { "User doesn't exist!" }
                };
            }

            var data = ExpoContext.Expos
                .Include(expo => expo.Location)
                .Include(expo => expo.Organiser)
                .Where(expo => expo.Organiser.UserName == user.UserName)
                .AsEnumerable();

            return new Response<IEnumerable<ExpoVM>>()
            {
                Message = "Expos retrieved!",
                Status = "success",
                IsSuccess = true,
                Data = Mapper.Map<IEnumerable<ExpoVM>>(data)
            };
        }

        private bool canEditExpo(IdentityUser organiser, Expo expo)
        {
            return organiser.Id == expo.OrganiserId;
        }

        private async Task<bool> isOrganiser(IdentityUser organiser)
        {
            return await UserManager.IsInRoleAsync(organiser, "Organizer");
        }

        private async Task<IEnumerable<Expo>> FilterData(IEnumerable<Expo> data, ExpoSearchModel expoSearch)
        {
            if (!string.IsNullOrWhiteSpace(expoSearch.Title))
            {
                data = data.Where(expo => expo.Title.ToLower().Contains(expoSearch.Title.ToLower()));
            }

            if (expoSearch.DateFrom is not null)
            {
                data = data.Where(expo => expo.DateOfOpening >= expoSearch.DateFrom);
            }
            
            if (expoSearch.DateTo is not null)
            {
                data = data.Where(expo => expo.DateOfClosing <= expoSearch.DateTo);
            }

            if (expoSearch.Industry is not null)
            {
                data = data.Where(expo => expo.IndustryId == expoSearch.Industry);
            }

            if (expoSearch.PriceFrom is not null)
            {
                data = data.Where(expo => expo.Tickets.Average(ticket => ticket.Price) >= expoSearch.PriceFrom);
            }
            
            if (expoSearch.PriceTo is not null)
            {
                data = data.Where(expo => expo.Tickets.Average(ticket => ticket.Price) <= expoSearch.PriceTo);
            }

            return data;
        }

        public async Task<Response> GetExpoById(int id)
        {
            var expoExist = await ExpoRepository.GetEntity(id);

            if (expoExist == null)
            {
                return new Response()
                {
                    Message = "Error while retrieving expo information!",
                    Status = "error",
                    IsSuccess = false,
                    Errors = new List<string>() { "Expo doesn't exist" }
                };
            }

            expoExist.Industry = await IndustryRepository.GetEntity(expoExist.IndustryId);
            expoExist.Location = await LocationRepository.GetEntity(expoExist.LocationId);
            var ticketTypess = await TicketTypeRepository.GetAll();

            expoExist.TicketTypes = ticketTypess.Where(ticket => ticket.ExpoId == expoExist.Id).ToList();
            
            return new Response<ExpoVM>()
            {
                Message = "Successfully retrieved expo information!",
                Status = "success",
                IsSuccess = true,
                Data = Mapper.Map<ExpoVM>(expoExist)
            };
        }

        public async Task<Response> AddLocation(LocationCreationVM locationCreation)
        {
            if (locationCreation == null)
            {
                return new Response()
                {
                    Message = "Failed to create location!",
                    Status = "error",
                    IsSuccess = false,
                    Errors = new List<string>() { "Invalid location data!" }
                };
            }

            var expo = await ExpoRepository.GetEntity(locationCreation.ExpoId);

            if (expo == null)
            {
                return new Response()
                {
                    Message = "Failed to create location!",
                    Status = "error",
                    IsSuccess = false,
                    Errors = new List<string>() { "Expo doesn't exist!" }
                };
            }

            try
            {
                var response = await LocationRepository.Add(Mapper.Map<Location>(locationCreation));

                expo.Location = response;
                expo.LocationId = response.Id;

                await ExpoRepository.Update(expo);

                return new Response<LocationVM>()
                {
                    Message = "Location successfully created!",
                    Status = "success",
                    IsSuccess = true,
                    Data = Mapper.Map<LocationVM>(response)
                };
            }
            catch (Exception exc)
            {
                return new Response()
                {
                    Message = "Failed to create location!",
                    Status = "error",
                    IsSuccess = false,
                    Errors = new List<string>() { exc.InnerException?.Message }
                };
            }
        }

        public async Task<Response> UpdateLocation(int id, LocationUpdateVM locationUpdate)
        {
            var location = await LocationRepository.GetEntity(id);

            if (location == null)
            {
                return new Response()
                {
                    Message = "Failed to update location!",
                    Status = "error",
                    IsSuccess = false,
                    Errors = new List<string>() { "Location doesn't exist!" }
                };
            }

            var expo = await ExpoRepository.GetEntity(locationUpdate.ExpoId);

            if (expo == null)
            {
                return new Response()
                {
                    Message = "Failed to create location!",
                    Status = "error",
                    IsSuccess = false,
                    Errors = new List<string>() { "Expo doesn't exist!" }
                };
            }

            location.Adress = locationUpdate.Adress;
            location.City = locationUpdate.City;
            location.ZipCode = locationUpdate.ZipCode;
            location.Country = locationUpdate.Country;

            try
            {
                var response = await LocationRepository.Update(location);

                return new Response<LocationVM>()
                {
                    Message = "Location successfully updated!",
                    Status = "success",
                    IsSuccess = true,
                    Data = Mapper.Map<LocationVM>(response)
                };
            }
            catch (Exception exc)
            {
                return new Response()
                {
                    Message = "Failed to update location!",
                    Status = "error",
                    IsSuccess = false,
                    Errors = new List<string>() { exc.InnerException?.Message }
                };
            }
        }

        public async Task<Response> HasOrganizedExpo(int id, string username)
        {
            var user = await UserManager.FindByNameAsync(username);

            if (user == null)
            {
                return new Response<bool>()
                {
                    Message = "Checkig if user is in role!",
                    Status = "NA",
                    IsSuccess = false,
                    Data = false,
                    Errors = new List<string>() { "User doesn't exist!" }
                };
            }

            var result = await UserManager.IsInRoleAsync(user, "Organizer");

            if(!result)
            {
                return new Response<bool>()
                {
                    Message = "Checkig if user can edit expo!",
                    Status = "NA",
                    IsSuccess = false,
                    Data = false,
                    Errors = new List<string>() { "User doesn't have Organizer permissions!" }
                };
            }

            var expo = await ExpoRepository.GetEntity(id);

            if (expo == null)
            {
                return new Response<bool>()
                {
                    Message = "Checkig if user can edit expo!",
                    Status = "NA",
                    IsSuccess = false,
                    Data = false,
                    Errors = new List<string>() { "Expo doesn't exist!" }
                };
            }

            if (expo.OrganiserId != user.Id)
            {
                return new Response<bool>()
                {
                    Message = "Checkig if user can edit expo!",
                    Status = "NA",
                    IsSuccess = false,
                    Data = false,
                    Errors = new List<string>() { "User doesn't permission to edit expo!" }
                };
            }

            return new Response<bool>()
            {
                Message = "Checkig if user is in role!",
                Status = "NA",
                IsSuccess = true,
                Data = true
            };
        }

        public async Task<Response> UpdateExpoAdmin(int id, ExpoUpdateVM input)
        {
            var expo = await ExpoRepository.GetEntity(id);

            if (expo is null)
            {
                return new Response()
                {
                    IsSuccess = false,
                    Message = "Expo update failed!",
                    Status = "error",
                    Errors = new List<string>() { "Expo doesn't exist!" }
                };
            }

            expo.Title = input.Title;
            expo.DateOfOpening = input.DateOfOpening;
            expo.DateOfClosing = input.DateOfClosing;

            var result = await ExpoRepository.Update(expo);

            var response = new Response<Expo>()
            {
                IsSuccess = true,
                Message = "Expo successfully updated!",
                Status = "success",
                Data = Mapper.Map<Expo>(result)
            };

            return response;
        }
 
    }
}
