using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace App.Automation.Pages.Locators;

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
            ? MobileBy.Id("com.companyname.weatherapp:id/search_results_list") // TODO: verify
            : MobileBy.AccessibilityId("SearchResultsList");                    // TODO: verify

    public static By FirstSearchResult(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.XPath("//android.widget.LinearLayout[@resource-id='com.companyname.weatherapp:id/search_result_item'][1]") // TODO: verify
            : MobileBy.AccessibilityId("SearchResultItem_0");                                                                        // TODO: verify

    public static By ShortSummaryCityLabel(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.Id("com.companyname.weatherapp:id/summary_city_label") // TODO: verify
            : MobileBy.AccessibilityId("SummaryCityLabel");                    // TODO: verify

    public static By ShortSummaryTemperature(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.Id("com.companyname.weatherapp:id/summary_temp_label") // TODO: verify
            : MobileBy.AccessibilityId("SummaryTempLabel");                    // TODO: verify

    public static By ShortSummaryConditionLabel(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.Id("com.companyname.weatherapp:id/summary_condition_label") // TODO: verify
            : MobileBy.AccessibilityId("SummaryConditionLabel");                    // TODO: verify

    public static By ShortSummaryCloseButton(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.Id("com.companyname.weatherapp:id/summary_close_button") // TODO: verify
            : MobileBy.AccessibilityId("SummaryCloseButton");                     // TODO: verify

    public static By ShortSummaryDetailsButton(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.Id("com.companyname.weatherapp:id/summary_details_button") // TODO: verify
            : MobileBy.AccessibilityId("SummaryDetailsButton");                     // TODO: verify

    public static By MapCanvas(string platform) =>
        PlatformHelper.IsAndroid(platform)
            ? MobileBy.Id("com.companyname.weatherapp:id/map_view")  // TODO: verify
            : MobileBy.AccessibilityId("MapView");                   // TODO: verify
}