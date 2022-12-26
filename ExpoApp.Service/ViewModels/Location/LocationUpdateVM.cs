using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpoApp.Service.ViewModels.Location
{
    public class LocationUpdateVM
    {
        public int Id { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string Country { get; set; }
        public int ExpoId { get; set; }
    }
}
