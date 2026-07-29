using App.Automation.Config;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace App.Automation.Pages;

public class AndroidDriverFactory : IDriverFactory
{
    private readonly AndroidConfig _config;
    private readonly string _appiumServerUrl;

    public AndroidDriverFactory(AndroidConfig config, string appiumServerUrl)
    {
        _config = config;
        _appiumServerUrl = appiumServerUrl;
    }
    
    public AppiumDriver CreateDriver()
    {
        var options = new AppiumOptions();

        options.PlatformName = _config.PlatformName;
        options.AutomationName = _config.AutomationName;
        options.DeviceName = _config.DeviceName;

        if (!string.IsNullOrWhiteSpace(_config.AppPath))
        {
            options.App = _config.AppPath;
        }

        options.AddAdditionalAppiumOption("appium:appPackage", _config.AppPackage);
        options.AddAdditionalAppiumOption("appium:appActivity", _config.AppActivity);
        options.AddAdditionalAppiumOption("appium:fullReset", _config.FullReset);

        return new AndroidDriver(new Uri(_appiumServerUrl), options);
    }
}