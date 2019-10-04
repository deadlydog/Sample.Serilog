# Serilog Samples

This repo contains a solution with a number of projects showing how to configure [Serilog](https://serilog.net) in them.

## NuGet packages added

- Microsoft.Extensions.Configuration.Json (to read from appsettings.json file)
- Serilog.Enrichers.Environment (optional)
- Serilog.Enrichers.Thread (optional)
- [Serilog.Exceptions](https://github.com/RehanSaeed/Serilog.Exceptions) (if you want exception details logged)
- Serilog.Settings.Configuration (to read from Microsoft.Extensions.Configuration)
- [Serilog.Sinks.Console](https://github.com/serilog/serilog-sinks-console) (to write to the console)
- [Serilog.Sinks.File](https://github.com/serilog/serilog-sinks-file) (to write to a file)

The `Enrichers` NuGet packages are only required if you want to enrich your logs with optional data.
Some Enrichers requires [some code changes](https://github.com/serilog/serilog/wiki/Enrichment).

## Configuration

It's best practice to configure your logging in a separate file, rather than directly on source code, so for all of the samples the configuration is set in the `appsettings.json` file.

__Note:__ You must set the file properties `Build Action` to `Content` and `Copy to Output Directory` to `Copy if newer` so that it gets copied to the app's bin directory.

The `Enrich` and `Properties` sections are pretty much the same; just the `Enrich` ones are out-of-the-box properties provided by the optional NuGet packages.

Some sinks do not display Properties (used by Enrichers) by default, such as the `Console` sink, and require you to explicitly define the output template and include `Properties`.
e.g. the `{Properties:j}` in:

```json
"WriteTo": [
    {
        "Name": "Console",
        "Args": {
            "theme": "Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme::Code, Serilog.Sinks.Console",
            "outputTemplate": "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} <s:{SourceContext}>{NewLine}{Exception} {Properties:j}{NewLine}"
        }
    }
],
```

[See the docs](https://github.com/serilog/serilog/wiki/Formatting-Output#formatting-plain-text) for more information on configuring the `outputTemplate`.

## Projects and what they show

### ConsoleApp.NetCore3 project

Shows how to use native Serilog without any abstractions to log to the console and file.
