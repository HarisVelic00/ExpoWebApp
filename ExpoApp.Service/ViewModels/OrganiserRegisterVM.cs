using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpoApp.Service.ViewModels
{
    public class OrganiserRegisterVM
    {
        [Required(ErrorMessage = "Company name is a required field!")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Email is a required field!")]
        [EmailAddress]
        [StringLength(30, MinimumLength = 8)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is a required field!")]
        [StringLength(40, MinimumLength = 8)]
        public string Password { get; set; }
    }
}
