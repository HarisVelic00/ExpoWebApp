using ExpoApp.Service.Interfaces;
using ExpoApp.Service.Shared;
using ExpoApp.Service.ViewModels.Tickets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpoWeb.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class TicketController : Controller
    {
        private readonly ITicketService TicketService;

        public TicketController (ITicketService ticketService)
        {
            TicketService = ticketService;
        }

        [HttpGet]
        public ActionResult <List<TicketVM>> GetAll()
        {
            return TicketService.GetTickets().ToList();
        }

        [HttpPost]
        [Authorize(Roles = "Organizer")]
        public async Task<ActionResult<Response<TicketVM>>> AddTicket([FromBody] TicketCreationVM karta)
        {
            var result = await TicketService.AddTicketType(karta);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        
        //[HttpPut("{id}")]
        //[Authorize(Roles = "Organizer")]
        //public async Task<ActionResult<Response<TicketVM>>> EditTicket(int id, [FromBody] TicketCreationVM ticketUpdateCreation)
        //{
        //    var result = await TicketService.EditTicket(id, ticketUpdateCreation);

        //    if (!result.IsSuccess)
        //    {
        //        return BadRequest(result);
        //    }

        //    return Ok(result);
        //}

        [HttpGet("{id}")]
        [Authorize(Roles = "Organizer")]
        public async Task<ActionResult<Response<List<TicketVM>>>> GetExpoTickets(int id)
        {
            var result = await TicketService.ExpoTickets(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Organizer")]
        public async Task<ActionResult<Response<List<TicketVM>>>> DelteTicket(int id)
        {
            var result = await TicketService.DeleteTicket(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Response>> PurchaseTicket(TicketPurchaseVM ticketPurchase)
        {
            var response = await TicketService.PurcahseTicket(ticketPurchase);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
