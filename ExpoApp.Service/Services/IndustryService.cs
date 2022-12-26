using AutoMapper;
using ExpoApp.Core.Models;
using ExpoApp.Repository.Context;
using ExpoApp.Repository.Repostiory;
using ExpoApp.Service.Interfaces;
using ExpoApp.Service.Shared;
using ExpoApp.Service.ViewModels.Industries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpoApp.Service.Services
{
    public class IndustryService : IIndustryService
    {
        private readonly IRepository<Industry> IndustryRepository;
        private readonly IMapper Mapper;
        private readonly ExpoContext _expocontext;

        public IndustryService(IRepository<Industry> industryRepository, IMapper mapper, ExpoContext expocontext)
        {
            IndustryRepository = industryRepository;
            Mapper = mapper;
            _expocontext = expocontext;
        }

        public async Task<Response> AddIndustry(IndustryCreationVM industryCreation)
        {
            if (AlreadyExists(industryCreation.Name))
            {
                return new Response()
                {
                    Message = "Failed to create industry!",
                    Status = "error",
                    IsSuccess = false,
                    Errors = new List<string>() { "Name already exists!!" }
                };
            }

            if (industryCreation == null)
            {
                return new Response()
                {
                    Message = "Failed to create industry!",
                    Status = "error",
                    IsSuccess = false,
                    Errors = new List<string>() { "Invalid industry data!" }
                };
            }

            try
            {
                var response = await IndustryRepository.Add(Mapper.Map<Industry>(industryCreation));


                return new Response<IndustryVM>()
                {
                    Message = "Industry successfully created!",
                    Status = "success",
                    IsSuccess = true,
                    Data = Mapper.Map<IndustryVM>(response)
                };
            }
            catch (Exception exc)
            {
                return new Response()
                {
                    Message = "Failed to create industry!",
                    Status = "error",
                    IsSuccess = false,
                    Errors = new List<string>() { exc.InnerException?.Message }
                };
            }
        }

        private bool AlreadyExists(string industry)
        {
            return _expocontext.Industry.Any(x => x.Name == industry);
        }




        public async Task<Response> UpdateIndustry(int id, IndustryUpdateVM industryUpdate)
        {
            var industry = await IndustryRepository.GetEntity(id);

            if (AlreadyExists(industryUpdate.Name))
            {
                 return new Response()
                {
                    Message = "Failed to update industry!",
                    Status = "error",
                    IsSuccess = false,
                    Errors = new List<string>() { "Industry name doesn't exists!" }
                };
            }

            if (industry == null)
            {
                return new Response()
                {
                    Message = "Failed to update industry!",
                    Status = "error",
                    IsSuccess = false,
                    Errors = new List<string>() { "Industry doesn't exist!" }
                };
            }

            industry.Name = industryUpdate.Name;

            try
            {
                var response = await IndustryRepository.Update(industry);

                return new Response<IndustryVM>()
                {
                    Message = "Industry successfully updated!",
                    Status = "success",
                    IsSuccess = true,
                    Data = Mapper.Map<IndustryVM>(response)
                };
            }
            catch (Exception exc)
            {
                return new Response()
                {
                    Message = "Failed to update Industry!",
                    Status = "error",
                    IsSuccess = false,
                    Errors = new List<string>() { exc.InnerException?.Message }
                };
            }

        }

        public IEnumerable<IndustryVM> GetIndustry()
        {
            var industry = IndustryRepository.GetAll();
            return Mapper.Map<IEnumerable<IndustryVM>>(industry.Result);
        }

        public async Task<Response> DeleteIndustry(int id)
        {
            var industry = await IndustryRepository.GetEntity(id);

            if (industry == null)
            {
                return new Response()
                {
                    IsSuccess = false,
                    Message = "Failed to delete industry!",
                    Status = "error",
                    Errors = new List<string>() { "Industry doesn't exist!" }
                };
            }

            try
            {
                await IndustryRepository.Delete(industry);
            }
            catch (Exception exc)
            {
                return new Response()
                {
                    IsSuccess = false,
                    Message = "Failed to delete industry!",
                    Status = "error",
                    Errors = new List<string>() { exc.InnerException.Message }
                };
            }

            return new Response()
            {
                IsSuccess = true,
                Message = "Industry deleted successfully",
                Status = "Success",
            };
        }

    }
}
