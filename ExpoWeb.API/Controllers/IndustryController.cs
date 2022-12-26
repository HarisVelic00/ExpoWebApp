using Microsoft.AspNetCore.Mvc;
using ExpoApp.Service.Interfaces;
using ExpoApp.Service.Shared;
using System.Threading.Tasks;
using ExpoApp.Service.ViewModels.Industries;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace ExpoWeb.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class IndustryController : Controller
    {
        private readonly IIndustryService IndustryService;

        public IndustryController(IIndustryService industryService)
        {
            IndustryService = industryService;
        }

        [HttpPost]
        public async Task<ActionResult<Response>> AddIndustry(IndustryCreationVM industryCreation)
        {
            var result = await IndustryService.AddIndustry(industryCreation);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> UpdateIndustry(int id, IndustryUpdateVM industryUpdate)
        {
            var result = await IndustryService.UpdateIndustry(id, industryUpdate);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<List<IndustryVM>> GetIndustries()
        {
            return IndustryService.GetIndustry().ToList();
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<Response<List<IndustryVM>>>> DeleteIndustries(int id)
        {
            var result = await IndustryService.DeleteIndustry(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }



    }
}