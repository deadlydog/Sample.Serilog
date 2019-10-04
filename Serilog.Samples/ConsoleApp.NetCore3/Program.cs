using Microsoft.Extensions.Configuration;
using Serilog;
using System;

namespace ConsoleApp.NetCore3
{
	class Program
	{
		static void Main(string[] args)
		{
			var configuration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.Build();

			Log.Logger = new LoggerConfiguration()
				.ReadFrom.Configuration(configuration)
				.CreateLogger();

			Log.Debug("Here's a Debug message.");
			Log.Information("Here's an Info message.");
			Log.Warning("Here's a Warning message.");
			Log.Error(new Exception("This is an exception"), "Here's an Error message.");
		}
	}
}
