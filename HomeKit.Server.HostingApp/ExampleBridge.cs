using HomeKit.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HomeKit.Server.HostingApp
{
    internal class ExampleBridge : BackgroundService
    {
        private readonly ILogger m_Logger;
        private readonly ILoggerFactory m_LoggerFactory;

        public ExampleBridge(ILogger<ExampleBridge> logger, ILoggerFactory loggerFactory)
        {
            m_Logger = logger;
            m_LoggerFactory = loggerFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // create bridge
            var bridge = new AccessoryBridge("Bridge");

            // create switch
            var switchAccessory = new Accessory("Switch");
            var switchService = switchAccessory.AddService<SwitchService>();
            switchService.On.Subscribe((_, newValue) => m_Logger.LogInformation("Switch was set to: {newValue}", newValue));

            // create fan
            var fanAccessory = new Accessory("Fan");
            var fanService = fanAccessory.AddService<Fanv2Service>();
            fanService.Active.Subscribe((_, newValue) => m_Logger.LogInformation("Fan active was set to: {newValue}", newValue));

            fanService.AddRotationSpeed(0);
            fanService.RotationSpeed.Subscribe((_, newValue) => m_Logger.LogInformation("Fan rotation speed was set to: {newValue}", newValue));

            // add to bridge
            bridge.Accessories.Add(switchAccessory);
            bridge.Accessories.Add(fanAccessory);

            await bridge.PublishAsync(new AccessoryServerOptions()
            {
                // optional logger factory for detailed log
                LoggerFactory = m_LoggerFactory,
                // optional persistent mac address
                MacAddress = "11:11:11:11:11:22",
                // persistent port
                // Port = 60622
                // other options:
                // IpAddress = "0.0.0.0"
                // StateDirectory = "/mnt/custom_state_directory"
                // ...
            });
        }
    }
}
