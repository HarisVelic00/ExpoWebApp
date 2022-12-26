using AutoMapper;
using ExpoApp.Core.Models;
using ExpoApp.Repository.Repostiory;
using ExpoApp.Service.Interfaces;
using ExpoApp.Service.Shared;
using ExpoApp.Service.ViewModels.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpoApp.Service.Services
{
    public class LocationService : ILocationService
    {
        private readonly IRepository<Location> LocationRepository;
        private readonly IMapper Mapper;

        public LocationService(IRepository<Location> locationRepository, IMapper mapper)
        {
            LocationRepository = locationRepository;
            Mapper = mapper;
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

            try
            {
                var response = await LocationRepository.Add(Mapper.Map<Location>(locationCreation));


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
    }
}
