using ExpoApp.Service.Interfaces;
using ExpoApp.Service.Shared;
using ExpoApp.Service.ViewModels.Tickets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ExpoWeb.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TicketTypeController : ControllerBase
    {
        private readonly ITicketTypeService TicketTypeService;

        public TicketTypeController(ITicketTypeService ticketTypeService)
        {
            TicketTypeService = ticketTypeService;
        }

        [HttpPost]
        [Authorize(Roles = "Organizer")]
        public async Task<ActionResult<Response>> CreateTicketType(TicketTypeCreationVM ticketTypeCreation)
        {
            var result = await TicketTypeService.CreateTicketType(ticketTypeCreation);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "Organizer")]
        public async Task<ActionResult<Response>> UpdateTicketType(TicketTypeUpdateVM ticketTypeCreaton)
        {
            var result = await TicketTypeService.UpdateTicketType(ticketTypeCreaton);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Organizer")]
        public async Task<ActionResult<Response>> DeleteTicketType(int id)
        {
            var result = await TicketTypeService.DeleteTicketType(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
