using ExpoApp.Service.Interfaces;
using ExpoApp.Service.Shared;
using ExpoApp.Service.ViewModels;
using ExpoApp.Service.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpoWeb.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService UserService;
    
        public AuthController(IUserService userService)
        {
            UserService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<TokenResponse>> Login(LoginVM login)
        {
            if (login is null)
            {
                return BadRequest(new { message = "Invalid credentials!" });
            }

            var token = await UserService.SignIn(login);

            if (token is null)
            {
                return BadRequest(new { message = "Login failed, please try again!" });
            }

            return Ok(token);
        }

    }
}
