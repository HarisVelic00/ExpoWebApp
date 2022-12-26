using ExpoApp.Repository.Context;
using ExpoApp.Repository.Repostiory;
using ExpoApp.Service.Interfaces;
using ExpoApp.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpoWeb.API.Extensions
{
    public static class AppServiceExtension
    {
        public static IServiceCollection ServiceExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ExpoContext>(options => options.UseSqlServer(configuration.GetConnectionString("DevBase")));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IExpoService, ExpoService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<ITicketTypeService, TicketTypeService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IIndustryService, IndustryService>();

            return services;
        }
    }
}
