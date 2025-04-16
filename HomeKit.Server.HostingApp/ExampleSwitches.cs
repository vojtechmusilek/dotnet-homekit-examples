using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeKit.Resources;
using HomeKit.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HomeKit.Server.HostingApp
{
    internal class ExampleSwitches : BackgroundService
    {
        private readonly ILogger m_Logger;

        public ExampleSwitches(ILogger<ExampleSwitches> logger)
        {
            m_Logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // create accessory
            var accessory = new Accessory("Switches", Category.Switch);

            // add 2 switches using AddService
            var switch1 = accessory.AddService<SwitchService>();
            var switch2 = accessory.AddService<SwitchService>();

            // or add existing Service directly to Services list
            // accessory.Services.Add(someService);

            // log value on change
            switch1.On.Changed += (_, newValue) => m_Logger.LogInformation($"Switch 1 was set to: {newValue}");
            switch2.On.Changed += (_, newValue) => m_Logger.LogInformation($"Switch 2 was set to: {newValue}");

            await accessory.PublishAsync(new AccessoryServerOptions()
            {
                // persistent mac address
                MacAddress = "11:11:11:11:11:11",
                // persistent port
                Port = 60611
                // other options:
                // IpAddress = "0.0.0.0"
                // StateDirectory = "/mnt/custom_state_directory"
                // ...
            });

            await Task.Delay(5000, stoppingToken);

            // when Value is set here it is sent to HomeKit clients
            switch1.On.Value = true;
        }
    }
}
