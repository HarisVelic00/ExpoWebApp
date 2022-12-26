using ExpoApp.NotificationWorker;
using ExpoApp.NotificationWorker.Extensions;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        var configuration = new ConfigurationBuilder().Build();

        services.ServiceExtension(configuration);
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
