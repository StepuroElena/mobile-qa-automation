using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace App.Automation.Pages;

public static class WeatherDetailsPageLocators
{
    private const string TemperaturePattern = @"-?\d+(\.\d+)?\s*°C";
    private const string TemperatureRangePattern = @"-?\d+(\.\d+)?\s*°C\s*/\s*-?\d+(\.\d+)?\s*°C";

    public static By CityTitle(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.TextView\").textMatches(\".*,.*,.*\")")
            : MobileBy.AccessibilityId("DetailsCityTitle"); // TODO: verify once iOS build is available

    public static By TemperatureCandidates(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator($"new UiSelector().className(\"android.widget.TextView\").textMatches(\"{TemperaturePattern}\")")
            : MobileBy.AccessibilityId("CurrentTempLabel"); // TODO: verify once iOS build is available

    public static By CurrentCondition(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.TextView\").instance(3)") 
            : MobileBy.AccessibilityId("CurrentConditionLabel"); // TODO: verify once iOS build is available

    public static By DailyForecastRow(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator($"new UiSelector().className(\"android.widget.TextView\").textMatches(\"{TemperatureRangePattern}\")")
            : MobileBy.AccessibilityId("DailyForecastRow"); // TODO: verify once iOS build is available

    public static By BackButton(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.Button\").text(\"‹\")")
            : MobileBy.AccessibilityId("BackButton"); // TODO: verify once iOS build is available
}