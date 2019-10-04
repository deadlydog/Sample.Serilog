# Serilog Samples

This repo contains a solution with a number of projects showing how to configure [Serilog](https://serilog.net) in them.

## ConsoleApp.NetCore3 project

Shows how to use native Serilog without any abstractions to log to the colored console and file.

NuGet packages added:

- Microsoft.Extensions.Configuration.Json
- Serilog.Settings.Configuration
- Serilog.Sinks.ColoredConsole
- Serilog.Sinks.File

Files added:

- `appsettings.json`
  - You must set the file properties `Build Action` to `Content` and `Copy to Output Directory` to `Copy if newer` so that it gets copied to the app's bin directory.
