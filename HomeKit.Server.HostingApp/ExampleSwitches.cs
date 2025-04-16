using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeKit.Resources;
using HomeKit.Services;
using Microsoft.Extensions.Hosting;

namespace HomeKit.Server.HostingApp
{
    internal class ExampleSwitches : BackgroundService
    {
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
            switch1.On.Changed += (sender, newValue) => Console.WriteLine($"switch1: {newValue}");
            switch2.On.Changed += (sender, newValue) => Console.WriteLine($"switch2: {newValue}");

            await accessory.PublishAsync(new AccessoryServerOptions()
            {
                // optional persistent mac address
                MacAddress = "11:11:11:11:11:11",
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
