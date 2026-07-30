using App.Automation.Pages.Locators;
using OpenQA.Selenium.Appium;

namespace App.Automation.Pages;

public class SettingsPage : BasePage
{
    private readonly string _platform;

    public SettingsPage(AppiumDriver driver) : base(driver)
    {
        PlatformHelper.EnsureSupportedPlatform(Platform);
        _platform = Platform;
    }

    public string GetLoggedInAsText()
    {
        Logger.Info("Getting 'Logged in as' text");
        return GetText(SettingsPageLocators.LoggedInAsLabel(_platform));
    }

    public void TapLightTheme()
    {
        Logger.Info("Tapping light theme button");
        Tap(SettingsPageLocators.LightThemeButton(_platform));
    }

    public void TapDarkTheme()
    {
        Logger.Info("Tapping dark theme button");
        Tap(SettingsPageLocators.DarkThemeButton(_platform));
    }

    public LoginPage TapLogout()
    {
        Logger.Info("Tapping 'Logout' button, navigating to LoginPage");
        return Tap<LoginPage>(SettingsPageLocators.LogoutButton(_platform));
    }

    public void TapBackButton()
    {
        Logger.Info("Tapping back button");
        Tap(SettingsPageLocators.BackButton(_platform));
    }

    public bool IsDisplayed()
    {
        Logger.Info("Checking if SettingsPage is displayed");
        return base.IsDisplayed(SettingsPageLocators.LogoutButton(_platform));
    }
}