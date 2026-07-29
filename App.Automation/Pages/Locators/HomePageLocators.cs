using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace App.Automation.Pages.Locators;

public static class HomePageLocators
{
    public static By GetStartedButton(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.Button\").text(\"Get Started\")")
            : MobileBy.AccessibilityId("GetStartedButton"); // TODO: verify once iOS build is available

    public static By SettingsIcon(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.Button\").instance(0)")
            : MobileBy.AccessibilityId("SettingsIcon"); // TODO: verify once iOS build is available
}