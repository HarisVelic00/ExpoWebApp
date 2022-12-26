using ExpoApp.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpoApp.NotificationWorker.Extensions
{
    public static class ServiceExtenstion
    {
        public static IServiceCollection ServiceExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ExpoContext>(options => options.UseSqlServer(configuration.GetConnectionString("DevBase")));

            return services;
        }
    }
}
