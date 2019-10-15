using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleAppNetCore3UsingMsLoggingAbstraction
{
	public class TheApp : IHostedService
	{
		ClassThatLogs _classThatLogs;

		public TheApp(ClassThatLogs classThatLogs)
		{
			_classThatLogs = classThatLogs ?? throw new ArgumentNullException(nameof(classThatLogs));
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			_classThatLogs.WriteLogs();

			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
