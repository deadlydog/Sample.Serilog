using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppNetCore3UsingMsLoggingAbstraction
{
	public class ClassThatLogs
	{
		private readonly ILogger<ClassThatLogs> _logger;

		public ClassThatLogs(ILogger<ClassThatLogs> logger)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public void WriteLogs()
		{
			var structuredData = new StructuredData();
			var simpleData = "This is a string.";

			_logger.LogTrace("Here's a Verbose message.");
			_logger.LogDebug("Here's a Debug message. Only Public Properties (not fields) are shown on structured data. Structured data: {@sampleData}. Simple data: {simpleData}.", structuredData, simpleData);
			_logger.LogInformation(new Exception("Exceptions can be put on all log levels"), "Here's an Info message.");
			_logger.LogWarning("Here's a Warning message.");
			_logger.LogError(new Exception("This is an exception."), "Here's an Error message.");
			_logger.LogCritical("Here's a Fatal message.");
		}
	}
}
