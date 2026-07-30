using App.Automation.Utils.Logger;
using Microsoft.Extensions.Configuration;

namespace App.Automation.Config;

public static class ConfigReader
{
    private static AppSettings? _cached;
    private static readonly ITestLogger _logger;

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