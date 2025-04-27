using HomeKit.Resources;
using HomeKit.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HomeKit.Server.HostingApp
{
    internal class ExampleSwitches : BackgroundService
    {
        private readonly ILogger m_Logger;
        private readonly ILoggerFactory m_LoggerFactory;

        public ExampleSwitches(ILogger<ExampleSwitches> logger, ILoggerFactory loggerFactory)
        {
            m_Logger = logger;
            m_LoggerFactory = loggerFactory;
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
            // inline callback
            switch1.On.Subscribe((_, newValue) => m_Logger.LogInformation("Switch 1 was set to: {newValue}", newValue));
            // method reference
            switch2.On.Subscribe(Switched);
            // can unsubscribe later
            //switch2.On.Unsubscribe(Switched);

            await accessory.PublishAsync(new AccessoryServerOptions()
            {
                // optional logger factory for detailed log
                LoggerFactory = m_LoggerFactory,
                // persistent mac address
                MacAddress = "11:11:11:11:11:11",
                // persistent port
                // Port = 60611
                // other options:
                // IpAddress = "0.0.0.0"
                // StateDirectory = "/mnt/custom_state_directory"
                // ...
            });

            await Task.Delay(5000, stoppingToken);

            // when Value is set here it is sent to HomeKit clients
            switch1.On.Value = true;
        }

        private void Switched(Characteristic characteristic, bool newValue)
        {
            m_Logger.LogInformation("Switch 2 was set to: {newValue}", newValue);
        }
    }
}
