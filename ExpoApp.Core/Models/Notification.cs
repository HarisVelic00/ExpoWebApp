using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpoApp.Core.Models
{
    public  class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsSeen { get; set; }
    }
}
