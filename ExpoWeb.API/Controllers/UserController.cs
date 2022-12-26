using ExpoApp.Service.Interfaces;
using ExpoApp.Service.Services;
using ExpoApp.Service.Shared;
using ExpoApp.Service.ViewModels;
using ExpoApp.Service.ViewModels.Industries;
using ExpoApp.Service.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpoWeb.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService UserService { get; set; }

        public UserController(IUserService userService)
        {
            UserService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Response>> Register([FromBody] OrganiserRegisterVM register)
        {
            if (register == null)
            {
                return BadRequest("Invalid credentials!");
            }

            Response response = await UserService.SignUp(register);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        
        [HttpGet("{username}")]
        public async Task<ActionResult<Response>> IsOrganizer(string username)
        {
            var result = await UserService.IsOrganizer(username);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserVM>>> GetAllUsers()
        {
            var users = await UserService.GetAllUsers();
            return Ok(users);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var result = await UserService.AdminDeleteUser(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> UpdateUsers(string id, UserUpdateVM userUpdate)
        {
            var result = await UserService.UpdateUsers(id, userUpdate);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }
}

