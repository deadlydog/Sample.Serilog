using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace ConsoleAppNetCore3UsingMsLoggingAbstraction
{
	class Program
	{
		static void Main(string[] args)
		{
			SetupStaticLogger();
			
			try
			{
				CreateHostBuilder(args).RunConsoleAsync();
			}
			catch (Exception ex)
			{
				Log.Fatal(ex, "An unhandled exception occurred.");
			}
			finally
			{
				Log.CloseAndFlush();
			}
		}

		private static void SetupStaticLogger()
		{
			var configuration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.Build();

			Log.Logger = new LoggerConfiguration()
				.ReadFrom.Configuration(configuration)
				.CreateLogger();
		}

		private static IHostBuilder CreateHostBuilder(string[] args) =>
			new HostBuilder()
				.ConfigureServices((hostContext, services) =>
				{
					services
						.AddTransient(typeof(ClassThatLogs))
						.AddHostedService<TheApp>();
				}
				)
				.ConfigureLogging((hostContext, logging) =>
				{
					logging.AddSerilog();
				}
			);
	}
}
