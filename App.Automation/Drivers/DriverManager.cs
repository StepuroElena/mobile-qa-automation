using App.Automation.Config;
using App.Automation.Utils.Logger;
using OpenQA.Selenium.Appium;

namespace App.Automation.Pages;

public class DriverManager
{
    private static readonly ThreadLocal<AppiumDriver?> _driver = new();
    private static readonly ITestLogger _logger = new SerilogTestLogger();

    public static AppiumDriver GetDriver(AppSettings settings)
    {
        if (_driver.Value is null)
        {
            IDriverFactory factory = settings.Platform.Equals("iOS", StringComparison.OrdinalIgnoreCase)
                ? new IosDriverFactory(settings.iOS, settings.Execution.AppiumServerUrl)
                : new AndroidDriverFactory(settings.Android, settings.Execution.AppiumServerUrl);

            _driver.Value = factory.CreateDriver();
            _logger.Info($"Driver created for platform: {settings.Platform}");
        }

        return _driver.Value;
    }

    public static void QuitDriver()
    {
        _driver.Value?.Quit();
        _driver.Value = null;
        _logger.Info("Driver quit");
    }
}