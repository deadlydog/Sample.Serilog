# Serilog Samples

This repo contains a solution with a number of projects showing how to configure [Serilog](https://serilog.net) in them.

## ConsoleApp.NetCore3 project

Shows how to use native Serilog without any abstractions to log to the colored console and file.

### NuGet packages added

- Microsoft.Extensions.Configuration.Json (to read from appsettings.json file)
- Serilog.Enrichers.Environment (optional)
- Serilog.Enrichers.Thread (optional)
- Serilog.Exceptions (if you want exception details logged)
- Serilog.Settings.Configuration (to read from Microsoft.Extensions.Configuration)
- Serilog.Sinks.ColoredConsole (to write to the console)
- Serilog.Sinks.File (to write to a file)

The `Enrichers` NuGet packages are only required if you want to enrich your logs with optional data.
Some Enrichers requires [some code changes](https://github.com/serilog/serilog/wiki/Enrichment).
Some sinks do not display Properties (used by Enrichers) by default, such as the `Console` sink, and require you to explicitly define the output format template and include `Properties`.


### Configuration

Configuration is set in the `appsettings.json` file.

__Note:__ You must set the file properties `Build Action` to `Content` and `Copy to Output Directory` to `Copy if newer` so that it gets copied to the app's bin directory.

The `Enrich` and `Properties` sections are pretty much the same; just the `Enrich` ones are out-of-the-box properties provided by the optional NuGet packages.
