using ExpoApp.Service.Shared;
using ExpoApp.Service.ViewModels;
using ExpoApp.Service.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpoApp.Service.Interfaces
{
    public interface IUserService
    {
        Task<TokenResponse> SignIn(LoginVM login);
        Task<Response> SignUp(OrganiserRegisterVM register);
        Task<Response<bool>> IsOrganizer(string username);
        Task<IEnumerable<UserVM>> GetAllUsers();
        
        Task<Response> AdminDeleteUser(string id);
        Task<Response> UpdateUsers(string id, UserUpdateVM user);
    }
}
