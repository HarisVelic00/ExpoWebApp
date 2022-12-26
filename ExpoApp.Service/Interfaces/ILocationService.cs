using ExpoApp.Service.Shared;
using ExpoApp.Service.ViewModels.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpoApp.Service.Interfaces
{
    public interface ILocationService
    {
        Task<Response> AddLocation(LocationCreationVM locationCreation);
        Task<Response> UpdateLocation(int id, LocationUpdateVM locationUpdate);
    }
}
