using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpoApp.Service.Interfaces
{
    public interface IHubClient
    {
        Task BroadCastMessage();
    }
}
