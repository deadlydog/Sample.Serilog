using Serilog;
using Serilog.Events;

// Setup a separate bootstrapper logger to log any issues that occur during initialization.
// This logger will be replaced by one that uses the config from the appsettings.json file later.
// We use this because as of .Net 6, the WebApplication builder automatically loads configuration
//	from multiple places, so we no longer need to explicitly define it.
//	More info: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-6.0#default-application-configuration-sources
Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
	.Enrich.FromLogContext()
	.WriteTo.Console()
	.CreateBootstrapLogger();

try
{
	Log.Information("Starting web host");
	BuildAndRunWebApp(args);
	return 0;
}
catch (Exception ex)
{
	Log.Fatal(ex, "Host terminated unexpectedly");
	return 1;
}
finally
{
	Log.CloseAndFlush();
}

static void AddSerilogConfiguration(WebApplicationBuilder builder)
{
	// NOTE: In .Net 6 the UseSerilog method now shows up on the Host; in .Net Core 3 it was on the Builder.
	builder.Host.UseSerilog((hostContext, services, loggerConfiguration) =>
	{
		loggerConfiguration
			.ReadFrom.Configuration(hostContext.Configuration)
			.ReadFrom.Services(services)
			.Enrich.FromLogContext();
		//.WriteTo.Console();
	});
}

static void BuildAndRunWebApp(string[] args)
{
	var builder = WebApplication.CreateBuilder(args);

	AddSerilogConfiguration(builder);

	// Add services to the container.
	builder.Services.AddRazorPages();

	var app = builder.Build();

	// Configure the HTTP request pipeline.
	if (!app.Environment.IsDevelopment())
	{
		app.UseExceptionHandler("/Error");
	}
	app.UseStaticFiles();

	app.UseRouting();

	app.UseAuthorization();

	app.MapRazorPages();

	app.Run();
}
