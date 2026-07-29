using App.Automation.Config;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;

namespace App.Automation.Pages;

public class IosDriverFactory : IDriverFactory
{
    private readonly IosConfig _config;
    private readonly string _appiumServerUrl;

    public IosDriverFactory(IosConfig config, string appiumServerUrl)
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
        options.PlatformVersion = _config.PlatformVersion;

        options.AddAdditionalAppiumOption("appium:bundleId", _config.BundleId);
        options.AddAdditionalAppiumOption("appium:fullReset", _config.FullReset);

        return new IOSDriver(new Uri(_appiumServerUrl), options);
    }
}