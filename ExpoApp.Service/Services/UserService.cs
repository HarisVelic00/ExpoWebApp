using AutoMapper;
using ExpoApp.Core.Models;
using ExpoApp.Repository.Context;
using ExpoApp.Repository.Repostiory;
using ExpoApp.Service.Interfaces;
using ExpoApp.Service.Shared;
using ExpoApp.Service.ViewModels;
using ExpoApp.Service.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpoApp.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> UserManager;
        private readonly SignInManager<IdentityUser> SignInManager;
        private readonly ITokenService TokenService;
        private readonly IRepository<IdentityUser> identityUser;
        private readonly IMapper Mapper;
        private readonly ExpoContext _expocontext;
        public UserService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, ITokenService tokenService, IRepository<IdentityUser> identityUser, IMapper mapper, ExpoContext expocontext)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            TokenService = tokenService;
            this.identityUser = identityUser;
            Mapper = mapper;
            _expocontext = expocontext;
        }

        public async Task<Response<bool>> IsOrganizer(string username)
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

            return new Response<bool>()
            {
                Message = "Checkig if user is in role!",
                Status = "NA",
                IsSuccess = result,
                Data = result
            };
        }

        public async Task<TokenResponse> SignIn(LoginVM login)
        {
            var userExists = await UserManager.FindByNameAsync(login.UserName);

            if (userExists == null)
            {
                return null;
            }

            var result = await SignInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);

            if (!result.Succeeded)
            {
                return null;
            }

            TokenResponse token = await TokenService.BuildToken(userExists);

            return token;
        }

        public async Task<Response> SignUp(OrganiserRegisterVM register)
        {
            var userExists = await UserManager.FindByNameAsync(register.CompanyName);

            if (userExists != null)
            {
                //Status = "Error", Message = "User already exists!" }

                return new Response()
                {
                    Status = "Error",
                    Message = "Company name has been taken, try another company name!",
                    IsSuccess = false
                };
            }

            Organiser user = new Organiser()
            {
                UserName = register.CompanyName,
                Email = register.Email
            };

            var result = await UserManager.CreateAsync(user, register.Password);
            
            if (!result.Succeeded)
            {
                return new Response()
                {
                    Status = "Error",
                    Message = "Invalid credenitals!",
                    IsSuccess = false,
                    Errors = result.Errors.Select(err => err.Description).ToList()
                };
            }

            var addToRoleResult = await UserManager.AddToRoleAsync(user, "Organizer");

           return new Response()
            {
                Status = "Success",
                Message = "User successfully created!",
                IsSuccess = true,
                Errors = addToRoleResult.Errors.Select(err => err.Description)
            };
        }

        public async Task<IEnumerable<UserVM>> GetAllUsers()
        {
            var users = await identityUser.GetAll();
            return Mapper.Map<IEnumerable<UserVM>>(users);
        }

        public async Task<Response> AdminDeleteUser(string id)
        {
            var user = await identityUser.GetEntity(id);

            if (user == null)
            {
                return new Response()
                {
                    IsSuccess = false,
                    Message = "Failed to delete user!",
                    Status = "error",
                    Errors = new List<string>() { "User doesn't exist!" }
                };
            }

            try
            {
                await identityUser.Delete(user);
            }
            catch (Exception exc)
            {
                return new Response()
                {
                    IsSuccess = false,
                    Message = "Failed to delete user!",
                    Status = "error",
                    Errors = new List<string>() { exc.InnerException?.Message }
                };
            }

            return new Response()
            {
                IsSuccess = true,
                Message = "user deleted successfully!",
                Status = "success"
            };
        }

        public async Task<Response> UpdateUsers(string id, UserUpdateVM user)
        {
            var users = await identityUser.GetEntity(id);

            if (AlreadyExists(user.Username))
            {
                return new Response()
                {
                    Message = "Failed to update user!",
                    Status = "error",
                    IsSuccess = false,
                    Errors = new List<string>() { "User name doesn't exists!" }
                };
            }

            if (users == null)
            {
                return new Response()
                {
                    Message = "Failed to update user!",
                    Status = "error",
                    IsSuccess = false,
                    Errors = new List<string>() { "User doesn't exist!" }
                };
            }

            users.UserName = user.Username;
            users.Email = user.Email;

            try
            {
                var response = await identityUser.Update(users);

                return new Response<UserVM>()
                {
                    Message = "User successfully updated!",
                    Status = "success",
                    IsSuccess = true,
                    Data = Mapper.Map<UserVM>(response)
                };
            }
            catch (Exception exc)
            {
                return new Response()
                {
                    Message = "Failed to update user!",
                    Status = "error",
                    IsSuccess = false,
                    Errors = new List<string>() { exc.InnerException?.Message }
                };
            }

        }

        private bool AlreadyExists(string username)
        {
            return _expocontext.Users.Any(x => x.UserName == username);
        }
    }
}
