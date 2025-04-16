using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HomeKit.Server.HostingApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args);

            builder.ConfigureServices((context, services) =>
            {
                services.AddHostedService<ExampleSwitches>();
            });

            var host = builder.Build();
            host.Run();
        }
    }
}