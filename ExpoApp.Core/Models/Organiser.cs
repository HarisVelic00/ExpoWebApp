using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpoApp.Core.Models
{
    [Table("Organizers")]
    public class Organiser : IdentityUser
    {
        public string Title { get; set; }
        public string AboutUs { get; set; }
        //public Location Location { get; set; }
    }
}
