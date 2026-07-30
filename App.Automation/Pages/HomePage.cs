using App.Automation.Pages.Locators;
using OpenQA.Selenium.Appium;

namespace App.Automation.Pages;

public class HomePage : BasePage
{
    private readonly string _platform;

    public HomePage(AppiumDriver driver) : base(driver)
    {
        PlatformHelper.EnsureSupportedPlatform(Platform);
        _platform = Platform;
    }

    public MapPage TapGetStarted()
    {
        Logger.Info("Tapping 'Get Started' button, navigating to MapPage");
        return Tap<MapPage>(HomePageLocators.GetStartedButton(_platform));
    }

    public SettingsPage TapSettingsIcon()
    {
        Logger.Info("Tapping settings icon, navigating to SettingsPage");
        return Tap<SettingsPage>(HomePageLocators.SettingsIcon(_platform));
    }

    public bool IsDisplayed()
    {
        Logger.Info("Checking if HomePage is displayed");
        return base.IsDisplayed(HomePageLocators.GetStartedButton(_platform));
    }
}