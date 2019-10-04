using Microsoft.Extensions.Configuration;
using Serilog;
using System;

namespace ConsoleApp.NetCore3
{
	partial class Program
	{
		static void Main(string[] args)
		{
			var configuration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.Build();

			Log.Logger = new LoggerConfiguration()
				.ReadFrom.Configuration(configuration)
				.CreateLogger();

			var structuredData = new StructuredData();
			var simpleData = "This is a string.";

			Log.Debug("Here's a Debug message. Structured data: {@sampleData}. Simple data: {simpleData}.", structuredData, simpleData);
			Log.Information("Here's an Info message. Structured data: {@sampleData}. Simple data: {simpleData}.", structuredData, simpleData);
			Log.Warning("Here's a Warning message. Structured data: {@sampleData}. Simple data: {simpleData}.", structuredData, simpleData);
			Log.Error(new Exception("This is an exception."), "Here's an Error message. Structured data: {@sampleData}. Simple data: {simpleData}.", structuredData, simpleData);
		}
	}
}
