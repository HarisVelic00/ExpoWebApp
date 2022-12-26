using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpoApp.Service.ViewModels.Location
{
    public class LocationCreationVM
    {
        public string Address { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string Country { get; set; }
        public int ExpoId { get; set; }
    }
}
