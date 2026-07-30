using App.Automation.Utils.Logger;
using Microsoft.Extensions.Configuration;

namespace App.Automation.Config;

public static class ConfigReader
{
    private static AppSettings? _cached;
    private static readonly ITestLogger _logger = new SerilogTestLogger();

    public static AppSettings Load()
    {
        if (_cached is not null)
        {
            return _cached;
        }

        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("Config/appsettings.json", optional: false, reloadOnChange: false)
            .AddEnvironmentVariables()
            .Build();

        var settings = new AppSettings();
        configuration.GetSection("AppSettings").Bind(settings);
        ValidateSettings(settings);

        _logger.Info($"Platform: {settings.Platform}");

        var platformSettings = settings.Platform.Equals("Android", StringComparison.OrdinalIgnoreCase)
            ? (object)settings.Android
            : settings.iOS;

        foreach (var prop in platformSettings.GetType().GetProperties())
        {
            _logger.Info($"PLatform settings: {settings.Platform}.{prop.Name}: {prop.GetValue(platformSettings)}");
        }

        foreach (var prop in settings.Execution.GetType().GetProperties())
        {
            _logger.Info($"Execution.{prop.Name}: {prop.GetValue(settings.Execution)}");
        }
        
        _cached = settings;
        return _cached;
    }

    private static void ValidateSettings(AppSettings settings)
    {
        if (string.IsNullOrWhiteSpace(settings.Execution.AppiumServerUrl))
            throw new InvalidOperationException("Appium_Server_Url did not set in Config/appsettings.json");


        if (string.IsNullOrWhiteSpace(settings.Platform))
            throw new InvalidOperationException("PLATFORM did not set in Config/appsettings.json.");
    }
}