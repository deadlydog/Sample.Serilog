# Serilog Samples

This repo contains a solution with a number of projects showing how to configure [Serilog](https://serilog.net) in them.

## NuGet packages required

To be able to define the Serilog configuration in a json file, rather than hard-coding it, use:

- Microsoft.Extensions.Configuration.Json (to read from appsettings.json file)
- Serilog.Settings.Configuration (to read from Microsoft.Extensions.Configuration)

Then add NuGet packages for whatever sinks you want to use:

- [Serilog.Sinks.Async](https://github.com/serilog/serilog-sinks-async) (use to log to other sinks asynchronously to improve performance)
- [Serilog.Sinks.Console](https://github.com/serilog/serilog-sinks-console) (to write to the console)
- [Serilog.Sinks.File](https://github.com/serilog/serilog-sinks-file) (to write to a file (supports rolling files))

### Enricher to automatically attach additional data to log entries

The `Enrichers` NuGet packages are optional and only required if you want to automatically enrich _all_ of your log entries with additional data.

- [Serilog.Enrichers.AspNetCoreHttpContext](https://github.com/trenoncourt/serilog-enrichers-aspnetcore-httpcontext) (for logging request data)
- [Serilog.Enrichers.Context](https://github.com/saleem-mirza/serilog-enrichers-context) (for Environmental variables)
- [Serilog.Enrichers.Environment](https://github.com/serilog/serilog-enrichers-environment) (for Machine or User name)
- [Serilog.Enrichers.Memory](https://github.com/JoshSchreuder/serilog-enrichers-memory) (for memory consumption)
- [Serilog.Enrichers.Process](https://github.com/serilog/serilog-enrichers-process) (for process ID and Name)
- [Serilog.Enrichers.Thread](https://github.com/serilog/serilog-enrichers-thread) (for Thread ID and Name)
- [Serilog.Exceptions](https://github.com/RehanSaeed/Serilog.Exceptions) (exception details)

There are many other Enricher NuGet packages for all sorts of things.

You can use the `LogContext` Enricher to attach custom properties to your logs, but it requires [code changes](https://github.com/serilog/serilog/wiki/Enrichment) to define what you want to attach.
An example might be to include a custom TransactionId property to link all logs from a specific transaction.

There's also packages like [Serilog.AspNetCore](https://github.com/serilog/serilog-aspnetcore) that can automatically attach web request IDs and other things to your logs.

__Note:__ Not all sinks show enricher properties by default. See the `Logging additional details` section below.

## Configuration

### Keep config in a file instead of code

It's best practice to configure your logging in a separate file, rather than directly on source code, so for all of the samples the configuration is set in the `appsettings.json` file.

__Note:__ You must set the file properties `Build Action` to `Content` and `Copy to Output Directory` to `Copy if newer` so that it gets copied to the app's bin directory and can be read by the app.

### Logging additional details

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

### Formatting the log output

[See the docs](https://github.com/serilog/serilog/wiki/Formatting-Output#formatting-plain-text) for more information on configuring the `outputTemplate`.

### File sync configuration

When using `"rollingInterval": "Day"` the date will automatically be appended to the file name.

### Json configuration example

```json
{
    "Serilog": {
        "MinimumLevel": "Verbose",
        "WriteTo": [
            {
                "Name": "Console",
                "Args": {
                    "theme": "Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme::Code, Serilog.Sinks.Console",
                    "outputTemplate": "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:j}{NewLine}{Properties:j}{NewLine}{Exception}"
                }
            },
            {
                "Name": "File",
                "Args": {
                    "restrictedToMinimumLevel": "Warning",
                    "path": "Logs\\log.txt",
                    "rollingInterval": "Day",
                    "fileSizeLimitBytes": 10240,
                    "rollOnFileSizeLimit": true,
                    "retainedFileCountLimit": 30
                }
            }
        ],
        "Enrich": [ "FromLogContext", "WithMachineName", "WithExceptionDetails" ],
        "Properties": {
            "ApplicationName": "SampleApp",
            "Environment": "Int"
        }
    }
}
```

Logging synchronously can slow down your application, especially logging to the console, so you'll typically want to enable asynchronous logging by using the `Async` sink and providing the other sinks in it's `Args`, like so:

```json
{
    "Serilog": {
        "MinimumLevel": "Verbose",
        "WriteTo": [
            {
                "Name": "Async",
                "Args": {
                    "configure": [
                        {
                            "Name": "Console",
                            "Args": {
                                "theme": "Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme::Code, Serilog.Sinks.Console",
                                "outputTemplate": "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:j}{NewLine}{Properties:j}{NewLine}{Exception}"
                            }
                        },
                        {
                            "Name": "File",
                            "Args": {
                                "restrictedToMinimumLevel": "Warning",
                                "path": "Logs\\log.txt",
                                "rollingInterval": "Day",
                                "fileSizeLimitBytes": 10240,
                                "rollOnFileSizeLimit": true,
                                "retainedFileCountLimit": 30
                            }
                        }
                    ]
                }
            }
        ],
        "Enrich": [ "FromLogContext", "WithMachineName", "WithExceptionDetails" ],
        "Properties": {
            "ApplicationName": "SampleApp",
            "Environment": "Int"
        }
    }
}
```

### Logging levels

As [defined in the config docs](https://github.com/serilog/serilog/wiki/Configuration-Basics#minimum-level), the logging levels are defined as:

- Verbose
- Debug
- Information
- Warning
- Error
- Fatal

__NOTE:__ While the levels logged can vary per sink via the `restrictedToMinimumLevel` property, the `MinimumLevel` property defines the absolute minimum level logged. So if the `MinimumLevel` was set to `Warning`, sinks could never log `Information`, `Debug`, or `Verbose` logs.

## Projects in the solution and what they show

### ConsoleAppNetCore3 project

Shows how to use native Serilog without any abstractions to log to the console and file.

The logging setup and custom logging is all done from `Program.cs`.

### AspNetCore3 project

This one also requires adding the `Serilog.AspNetCore` NuGet package.

The setup is done in `Program.cs` and custom logging is done from `Pages\Index.cshtml.cs`.
