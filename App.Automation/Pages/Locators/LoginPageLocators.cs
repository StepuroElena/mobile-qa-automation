using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace App.Automation.Pages.Locators;

public static class LoginPageLocators
{
    public static By EmailField(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.EditText\").instance(0)")
            : MobileBy.AccessibilityId("EmailEntry"); // TODO: verify once iOS build is available

    public static By PasswordField(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.EditText\").instance(1)")
            : MobileBy.AccessibilityId("PasswordEntry"); // TODO: verify once iOS build is available

    public static By LoginButton(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.Button\").text(\"Login\")")
            : MobileBy.AccessibilityId("LoginButton"); // TODO: verify once iOS build is available

    public static By RegisterLink(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.TextView\").textContains(\"Register\")")
            : MobileBy.AccessibilityId("RegisterLink"); // TODO: verify once iOS build is available

    public static By ErrorMessage(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.TextView\").text(\"Invalid email or password.\")")
            : MobileBy.AccessibilityId("LoginErrorLabel");  // TODO: verify once iOS build is available           
}