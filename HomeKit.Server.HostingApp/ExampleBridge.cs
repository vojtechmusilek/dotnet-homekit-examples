using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using HomeKit.Resources;
using HomeKit.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HomeKit.Server.HostingApp
{
    internal class ExampleBridge : BackgroundService
    {
        private readonly ILogger m_Logger;

        public ExampleBridge(ILogger<ExampleBridge> logger)
        {
            m_Logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // create bridge
            var bridge = new AccessoryBridge("Bridge");

            // create switch
            var switchAccessory = new Accessory("Switch");
            var switchService = switchAccessory.AddService<SwitchService>();
            switchService.On.Changed += (_, newValue) => m_Logger.LogInformation($"Switch was set to: {newValue}");

            // create fan
            var fanAccessory = new Accessory("Fan");
            var fanService = fanAccessory.AddService<Fanv2Service>();
            fanService.Active.Changed += (_, newValue) => m_Logger.LogInformation($"Fan active was set to: {newValue}");

            fanService.AddRotationSpeed(0);
            fanService.RotationSpeed.Changed += (_, newValue) => m_Logger.LogInformation($"Fan rotation speed was set to: {newValue}");

            // add to bridge
            bridge.Accessories.Add(switchAccessory);
            bridge.Accessories.Add(fanAccessory);

            await bridge.PublishAsync(new AccessoryServerOptions()
            {
                // optional persistent mac address
                MacAddress = "11:11:11:11:11:22",
                // persistent port
                Port = 60622
                // other options:
                // IpAddress = "0.0.0.0"
                // StateDirectory = "/mnt/custom_state_directory"
                // ...
            });
        }
    }
}
