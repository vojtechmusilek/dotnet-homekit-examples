using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HomeKit.Server.HostingApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args);

            builder.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.AddFilter("HomeKit.Mdns.MdnsClient", LogLevel.Warning);
            });

            builder.ConfigureServices((context, services) =>
            {
                services.AddHostedService<ExampleSwitches>();
                services.AddHostedService<ExampleBridge>();
            });

            var host = builder.Build();
            host.Run();
        }
    }
}
