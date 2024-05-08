using Serilog;
using Serilog.Formatting.Json;

namespace Catalog.API;

public static class SerilogConfiguration
{
    public static void ConfigureSerilog()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.File(new JsonFormatter(), "important.json", restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning)
            .WriteTo.File("all-.logs", rollingInterval: RollingInterval.Day)
            .MinimumLevel.Debug()
            .CreateLogger();
    }
}