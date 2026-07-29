using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace App.Automation.Pages.Locators;

public static class SettingsPageLocators
{
    public static By LoggedInAsLabel(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.TextView\").textStartsWith(\"Logged in as\")")
            : MobileBy.AccessibilityId("LoggedInAsLabel"); // TODO: verify once iOS build is available

    public static By LightThemeButton(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.Button\").textContains(\"Light\")")
            : MobileBy.AccessibilityId("LightThemeButton"); // TODO: verify once iOS build is available

    public static By DarkThemeButton(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.Button\").textContains(\"Dark\")")
            : MobileBy.AccessibilityId("DarkThemeButton"); // TODO: verify once iOS build is available

    public static By LogoutButton(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.Button\").text(\"Logout\")")
            : MobileBy.AccessibilityId("LogoutButton"); // TODO: verify once iOS build is available

    public static By BackButton(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.Button\").text(\"‹\")")
            : MobileBy.AccessibilityId("BackButton"); // TODO: verify once iOS build is available
}