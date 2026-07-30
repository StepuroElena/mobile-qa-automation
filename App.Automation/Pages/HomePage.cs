using App.Automation.Pages.Locators;
using OpenQA.Selenium.Appium;

namespace App.Automation.Pages;

public class HomePage : BasePage
{
    private readonly string _platform;

    public HomePage(AppiumDriver driver, int explicitWaitSeconds, string platform)
        : base(driver, explicitWaitSeconds, platform)
    {
        PlatformHelper.EnsureSupportedPlatform(platform);
        _platform = platform;
    }

    public MapPage TapGetStarted() => Tap<MapPage>(HomePageLocators.GetStartedButton(_platform));
    public SettingsPage TapSettingsIcon() => Tap<SettingsPage>(HomePageLocators.SettingsIcon(_platform));
    public bool IsDisplayed() => IsDisplayed(HomePageLocators.GetStartedButton(_platform));
}