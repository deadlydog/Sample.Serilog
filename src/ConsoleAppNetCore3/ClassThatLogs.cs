using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppNetCore3
{
	public class ClassThatLogs
	{
		public void WriteLogs()
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
