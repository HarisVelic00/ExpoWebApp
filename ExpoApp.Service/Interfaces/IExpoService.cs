using ExpoApp.Core.Models;
using ExpoApp.Service.SearchModels;
using ExpoApp.Service.Shared;
using ExpoApp.Service.ViewModels.Expo;
using ExpoApp.Service.ViewModels.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpoApp.Service.Interfaces
{
    public interface IExpoService
    {
        Task<Response> GetExpos(ExpoSearchModel expoSearch = null);
        Task<Response> GetExpoById(int id);
        Task<Response> AddExpoAsync(ExpoCreationVM input);
        Task<Response> UpdateExpoAsync(int id, ExpoUpdateVM input);
        Task<Response> DeleteExpoAsync(int id, string username);
        Task<Response> UserExpos(string username); 
        Task<Response> AddLocation(LocationCreationVM locationCreation);
        Task<Response> UpdateLocation(int id, LocationUpdateVM locationUpdate);
        Task<Response> HasOrganizedExpo(int id, string username);

        Task<Response> AdminDeleteExpo(int id);
        Task<Response> UpdateExpoAdmin(int id, ExpoUpdateVM expoUpdate);


    }
}
