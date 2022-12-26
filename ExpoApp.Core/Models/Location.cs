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
    public class Location
    {
        [Key]
        public int Id { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string Country { get; set; }

    }
}
