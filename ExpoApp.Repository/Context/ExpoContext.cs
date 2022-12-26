using ExpoApp.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpoApp.Repository.Context
{
    public class ExpoContext : IdentityDbContext
    {
        public ExpoContext(DbContextOptions<ExpoContext> options) : base(options) { }

        public DbSet<Expo> Expos { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Industry> Industry{get; set;}
    }
}
