using AutoMapper.Execution;
using ExpoApp.Core.Models;
using ExpoApp.Repository.Context;
using ExpoApp.Service.Interfaces;
using ExpoApp.Service.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Threading.Timer;

namespace ExpoWeb.API.NotificationWorker
{
    public class ExpoOpeningNotifier : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly ILogger<ExpoOpeningNotifier> _logger;
        private readonly IHubContext<BroadcastHub, IHubClient> HubContext;
        IServiceProvider ServiceProvider;

        public ExpoOpeningNotifier(
            ILogger<ExpoOpeningNotifier> logger,
            IHubContext<BroadcastHub, IHubClient> hubContext,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            HubContext = hubContext;
            ServiceProvider = serviceProvider;
        }

        private async void NotifiyExpoOpening(object state)
        {
            _logger.LogInformation("Expo notifier running!" + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));

            using (var scope = ServiceProvider.CreateScope())
            {
                var ExpoContext = scope.ServiceProvider.GetRequiredService<ExpoContext>();

                var expos = ExpoContext.Expos.Include(expo => expo.Tickets).Where(expo => 
                expo.DateOfOpening.Year == DateTime.Now.Year &&
                expo.DateOfOpening.Month == DateTime.Now.Month &&
                expo.DateOfOpening.Day == DateTime.Now.Day &&
                expo.DateOfOpening.Hour == DateTime.Now.Hour &&
                expo.DateOfOpening.Minute == DateTime.Now.Minute).ToList();

                foreach (var expo in expos)
                {
                    var notifitcations = expo.Tickets.Select(ticket => new Notification()
                    {
                        Title = "Opening today!",
                        Content = $"{expo.Title} is opening today!",
                        CreationDate = DateTime.Now,
                        IsSeen = false,
                        UserId = ticket.UserId
                    }).ToList();

                    ExpoContext.Notifications.AddRange(notifitcations);
                }

                await ExpoContext.SaveChangesAsync();
                await HubContext.Clients.All.BroadCastMessage();
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(
            NotifiyExpoOpening,
            null,
            TimeSpan.Zero,
            TimeSpan.FromMinutes(1));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
