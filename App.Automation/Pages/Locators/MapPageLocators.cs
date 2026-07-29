using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace App.Automation.Pages;

public static class MapPageLocators
{
    public static By SearchField(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.EditText\").text(\"Search\")")
            : MobileBy.AccessibilityId("MapSearchEntry"); // TODO: verify once iOS build is available

    public static By BackButton(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.Button\").text(\"‹\")")
            : MobileBy.AccessibilityId("BackButton"); // TODO: verify once iOS build is available

    public static By SearchResultsList(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator("new UiSelector().className(\"androidx.recyclerview.widget.RecyclerView\")")
            : MobileBy.AccessibilityId("SearchResultsList"); // TODO: verify once iOS build is available

    public static By FirstSearchResult(string platform, string cityName)
    {
        var capitalized = char.ToUpper(cityName[0]) + cityName[1..].ToLower();

        return PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator($"new UiSelector().className(\"android.widget.TextView\").textStartsWith(\"{capitalized}\")")
            : MobileBy.AccessibilityId("SearchResultItem_0"); // TODO: verify once iOS build is available
    }

    public static By ShortSummaryCityLabel(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.TextView\").instance(0)")
            : MobileBy.AccessibilityId("SummaryCityLabel"); // TODO: verify once iOS build is available

    public static By ShortSummaryTemperature(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.TextView\").instance(2)")
            : MobileBy.AccessibilityId("SummaryTempLabel"); // TODO: verify once iOS build is available

    public static By ShortSummaryConditionLabel(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.TextView\").instance(3)")
            : MobileBy.AccessibilityId("SummaryConditionLabel"); // TODO: verify once iOS build is available

    public static By ShortSummaryCloseButton(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.Button\").instance(0)")
            : MobileBy.AccessibilityId("SummaryCloseButton"); // TODO: verify once iOS build is available

    public static By ShortSummaryDetailsButton(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.Button\").text(\"Details\")")
            : MobileBy.AccessibilityId("SummaryDetailsButton"); // TODO: verify once iOS build is available
}