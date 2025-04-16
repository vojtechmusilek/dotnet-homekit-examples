# dotnet-homekit-examples

Examples for https://github.com/vojtechmusilek/dotnet-homekit

## [HomeKit.Server.HostingApp](https://github.com/vojtechmusilek/dotnet-homekit-examples/tree/main/HomeKit.Server.HostingApp)

- uses [Microsoft.Extensions.Hosting](https://www.nuget.org/packages/microsoft.extensions.hosting)
- uses [HomeKit.Server](https://www.nuget.org/packages/HomeKit.Server)
- [Program.cs](./HomeKit.Server.HostingApp/Program.cs) - basic hosting app setup with example hosting services registered
- [ExampleSwitches.cs](./HomeKit.Server.HostingApp/ExampleSwitches.cs) - accessory with 2 switches
- [ExampleBridge.cs](./HomeKit.Server.HostingApp/ExampleBridge.cs) - bridge with 2 accessories: switch and fan
