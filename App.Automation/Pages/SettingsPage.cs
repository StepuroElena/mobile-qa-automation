using App.Automation.Pages.Locators;
using OpenQA.Selenium.Appium;

namespace App.Automation.Pages;

public class SettingsPage : BasePage
{
    private readonly string _platform;

    public SettingsPage(AppiumDriver driver) : base(driver) => PlatformHelper.EnsureSupportedPlatform(Platform);

    public string GetLoggedInAsText() => GetText(SettingsPageLocators.LoggedInAsLabel(_platform));
    public void TapLightTheme() => Tap(SettingsPageLocators.LightThemeButton(_platform));
    public void TapDarkTheme() => Tap(SettingsPageLocators.DarkThemeButton(_platform));
    public LoginPage TapLogout() => Tap<LoginPage>(SettingsPageLocators.LogoutButton(_platform));
    public void TapBackButton() => Tap(SettingsPageLocators.BackButton(_platform));
    public bool IsDisplayed() => IsDisplayed(SettingsPageLocators.LogoutButton(_platform));
}