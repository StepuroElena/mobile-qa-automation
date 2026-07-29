using App.Automation.Config;
using Serilog;

namespace App.Automation.Utils.Logger;

public class LoggerBootstrapper
{
    private static bool _initialized;
    private static readonly object _lock = new();

    public static void Initialize(LoggingConfig config)
    {
        if (_initialized)
        {
            return;
        }

        lock (_lock)
        {
            if (_initialized)
            {
                return;
            }

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(
                    path: config.LogFilePath,
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "{Timestamp:HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.Console(
                    outputTemplate: "{Timestamp:HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .Enrich.FromLogContext()
                .CreateLogger();

            _initialized = true;
        }
    }

    public static void CloseAndFlush() => Log.CloseAndFlush();
}