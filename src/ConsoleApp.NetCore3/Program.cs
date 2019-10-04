using Microsoft.Extensions.Configuration;
using Serilog;
using System;

namespace ConsoleApp.NetCore3
{
	public class Program
	{
		static void Main(string[] args)
		{
			SetupStaticLogger();

			try
			{
				RunApp();
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

		private static void RunApp()
		{
			var structuredData = new StructuredData();
			var simpleData = "This is a string.";

			Log.Verbose("Here's a Verbose message.");
			Log.Debug("Here's a Debug message. Only Public Properties (not fields) are shown on structured data. Structured data: {@sampleData}. Simple data: {simpleData}.", structuredData, simpleData);
			Log.Information(new Exception("Exceptions can be put on all log levels"), "Here's an Info message.");
			Log.Warning("Here's a Warning message.");
			Log.Error(new Exception("This is an exception."), "Here's an Error message.");
			Log.Fatal("Here's a Fatal message.");
		}
	}
}
