using Microsoft.Extensions.Configuration;

namespace App.Automation.Config;

public class ConfigReader
{
    private static AppSettings? _cached;

    public static AppSettings Load()
    {
        if (_cached is not null)
        {
            return _cached;
        }

        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("Config/appsettings.json", optional: false, reloadOnChange: false)
            .Build();

        var settings = new AppSettings();
        configuration.GetSection("AppSettings").Bind(settings);

        _cached = settings;
        return _cached;
    }
}