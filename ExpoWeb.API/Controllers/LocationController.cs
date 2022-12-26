using ExpoApp.Service.Interfaces;
using ExpoApp.Service.Shared;
using ExpoApp.Service.ViewModels.Location;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ExpoWeb.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService LocationService;

        public LocationController(ILocationService locationService)
        {
            LocationService = locationService;
        }

        [HttpPost]
        public async Task<ActionResult<Response>> AddLocation(LocationCreationVM locationCreation)
        {
            var result = await LocationService.AddLocation(locationCreation);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> UpdateLocation(int id, LocationUpdateVM locationUpdate)
        {
            var result = await LocationService.UpdateLocation(id, locationUpdate);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
