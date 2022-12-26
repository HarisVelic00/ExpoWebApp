using ExpoApp.Core.Models;
using ExpoApp.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace ExpoApp.NotificationWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ExpoContext ExpoContext;

        public Worker(ILogger<Worker> logger, ExpoContext expoContext)
        {
            _logger = logger;
            ExpoContext = expoContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var expos = ExpoContext.Expos.Include(expo => expo.Tickets).Where(expo => expo.DateOfOpening == DateTime.Now).ToList();

                foreach(var expo in expos)
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

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}