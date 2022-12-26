using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpoApp.Service.Shared
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTime Expiriation { get; set; }
    }
}
