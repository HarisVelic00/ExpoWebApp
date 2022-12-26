using ExpoApp.Service.Shared;
using ExpoApp.Service.ViewModels.Industries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpoApp.Service.Interfaces
{
    public interface IIndustryService
    {
        Task<Response> AddIndustry(IndustryCreationVM industryCreation);
        Task<Response> UpdateIndustry(int id, IndustryUpdateVM industryUpdate);
        IEnumerable<IndustryVM> GetIndustry();
        Task<Response> DeleteIndustry(int id);
    }
}
