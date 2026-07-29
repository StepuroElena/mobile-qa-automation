using App.Automation.Pages.Locators;
using OpenQA.Selenium.Appium;

namespace App.Automation.Pages;

public class HomePage : BasePage
{
    private readonly string _platform;

    public HomePage(AppiumDriver driver, int explicitWaitSeconds, string platform)
        : base(driver, explicitWaitSeconds)
    {
        PlatformHelper.EnsureSupportedPlatform(platform);
        _platform = platform;
    }

    public void TapGetStarted() => Tap(HomePageLocators.GetStartedButton(_platform));
    public void TapSettingsIcon() => Tap(HomePageLocators.SettingsIcon(_platform));
    public bool IsDisplayed() => IsDisplayed(HomePageLocators.GetStartedButton(_platform));
}