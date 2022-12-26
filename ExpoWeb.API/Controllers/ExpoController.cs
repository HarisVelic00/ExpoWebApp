using ExpoApp.Service.Interfaces;
using ExpoApp.Service.SearchModels;
using ExpoApp.Service.Services;
using ExpoApp.Service.Shared;
using ExpoApp.Service.ViewModels.Expo;
using ExpoApp.Service.ViewModels.Location;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpoWeb.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class ExpoController : Controller
    {
        private IExpoService ExpoServis;

        public ExpoController(IExpoService expoServis)
        {
            ExpoServis = expoServis;
        }

        //Servis za ispis
        [HttpGet]
        public async Task<ActionResult<Response>> GetAll([FromQuery] ExpoSearchModel expoSearch)
        {
            var result = await ExpoServis.GetExpos(expoSearch);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetById(int id)
        {
            var result = await ExpoServis.GetExpoById(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        //servis sa dodavanje 
        [HttpPost]
        //[Authorize(Roles = "Organizer")]
        public async Task<ActionResult<Response>> AddExpo([FromBody] ExpoCreationVM expoCreation)
        {
            var result = await ExpoServis.AddExpoAsync(expoCreation);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Response>> UpdateExpo(int id, ExpoUpdateVM expoUpdate)
        {
            var result = await ExpoServis.UpdateExpoAsync(id, expoUpdate);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Organizer, Admin")]
        public async Task<ActionResult> DeleteExpo(int id, [FromBody] string username)
        {
            var result = await ExpoServis.DeleteExpoAsync(id, username);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteExpoAdmin(int id)
        {
            var result = await ExpoServis.AdminDeleteExpo(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpGet("{username}")]
        [Authorize(Roles = "Organizer")]
        public async Task<ActionResult<Response>> GetUserExpos(string username)
        {
            var result = await ExpoServis.UserExpos(username);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Organizer")]
        public async Task<ActionResult<Response>> AddExpoLocation(LocationCreationVM locationCreation)
        {
            var result = await ExpoServis.AddLocation(locationCreation);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Organizer")]
        public async Task<ActionResult<Response>> UpdateExpoLocation(int id, LocationUpdateVM locationUpdate)
        {
            var result = await ExpoServis.UpdateLocation(id, locationUpdate);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<Response>> CanEditExpo(int expoId, string username)
        {
            var resposne = await ExpoServis.HasOrganizedExpo(expoId, username);

            return Ok(resposne);
        }



        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> UpdateExpoAdmin(int id, ExpoUpdateVM expoUpdate)
        {
            var result = await ExpoServis.UpdateExpoAdmin(id, expoUpdate);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

    }
}
