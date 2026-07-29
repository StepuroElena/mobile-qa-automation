using App.Automation.Config;
using OpenQA.Selenium.Appium;

namespace App.Automation.Pages;

public class DriverManager
{
    private static readonly ThreadLocal<AppiumDriver?> _driver = new();

    public static AppiumDriver GetDriver(AppSettings settings)
    {
        if (_driver.Value is null)
        {
            IDriverFactory factory = settings.Platform.Equals("iOS", StringComparison.OrdinalIgnoreCase)
                ? new IosDriverFactory(settings.iOS, settings.Execution.AppiumServerUrl)
                : new AndroidDriverFactory(settings.Android, settings.Execution.AppiumServerUrl);

            _driver.Value = factory.CreateDriver();
        }

        return _driver.Value;
    }

    public static void QuitDriver()
    {
        _driver.Value?.Quit();
        _driver.Value = null;
    }
}