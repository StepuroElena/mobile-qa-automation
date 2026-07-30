using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace App.Automation.Pages.Locators;

 public static class RegistrationPageLocators
{
    public static By FullNameField(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.EditText\").instance(0)")
            : MobileBy.AccessibilityId("FullNameEntry"); // TODO: verify once iOS build is available

    public static By EmailField(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.EditText\").instance(1)")
            : MobileBy.AccessibilityId("RegisterEmailEntry"); // TODO: verify once iOS build is available

    public static By PasswordField(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.EditText\").instance(2)")
            : MobileBy.AccessibilityId("RegisterPasswordEntry"); // TODO: verify once iOS build is available

    public static By ConfirmPasswordField(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.EditText\").instance(3)")
            : MobileBy.AccessibilityId("ConfirmPasswordEntry"); // TODO: verify once iOS build is available

    public static By RegisterButton(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.Button\").text(\"Register\")")
            : MobileBy.AccessibilityId("RegisterButton"); // TODO: verify once iOS build is available

    public static By LoginLink(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.TextView\").text(\"Login\")")
            : MobileBy.AccessibilityId("BackToLoginLink"); // TODO: verify once iOS build is available

    public static By BackButton(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.Button\").text(\"<\")")
            : MobileBy.AccessibilityId("BackButton"); // TODO: verify once iOS build is available
    
    public static By ErrorRegistrationMessage(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.TextView\").textContains(\"email already exists\")")
            : MobileBy.AccessibilityId("RegistrationErrorLabel"); // TODO: verify once iOS build is available
    
    public static By ErrorPasswordMessage(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.TextView\").textContains(\"do not match\")")
            : MobileBy.AccessibilityId("RegistrationErrorLabel"); // TODO: verify once iOS build is available
    
    public static By SuccessMessage(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.TextView\").textContains(\"Account created successfully\")")
            : MobileBy.AccessibilityId("RegistrationSuccessLabel"); // TODO: verify once iOS build is available
}