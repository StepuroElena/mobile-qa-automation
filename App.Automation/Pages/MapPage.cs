using OpenQA.Selenium.Appium;

namespace App.Automation.Pages;

public class MapPage : BasePage
{
    private readonly string _platform;

    public MapPage(AppiumDriver driver, int explicitWaitSeconds, string platform)
        : base(driver, explicitWaitSeconds, platform)
    {
        PlatformHelper.EnsureSupportedPlatform(platform);
        _platform = platform;
    }

    public void EnterSearchQuery(string cityName) => TypeText(MapPageLocators.SearchField(_platform), cityName);
    public WeatherDetailsPage TapFirstSearchResult(string cityName) => Tap<WeatherDetailsPage>(MapPageLocators.FirstSearchResult(_platform, cityName));
    public HomePage TapBackButton() => Tap<HomePage>(MapPageLocators.BackButton(_platform));
    public bool IsDisplayed() => IsDisplayed(MapPageLocators.SearchField(_platform));

    public void TapMapNearCenter()
    {
        var windowSize = Driver.Manage().Window.Size;

        var x = (int)(windowSize.Width * 0.35);
        var y = (int)(windowSize.Height * 0.40);

        var args = new Dictionary<string, object>
        {
            { "x", x },
            { "y", y }
        };

        Driver.ExecuteScript("mobile: clickGesture", args);
    }

    public string GetShortSummaryCity() => GetText(MapPageLocators.ShortSummaryCityLabel(_platform));
    public string GetShortSummaryTemperature() => GetText(MapPageLocators.ShortSummaryTemperature(_platform));
    public string GetShortSummaryCondition() => GetText(MapPageLocators.ShortSummaryConditionLabel(_platform));
    public bool IsShortSummaryDisplayed() => IsDisplayed(MapPageLocators.ShortSummaryCityLabel(_platform));
    public void CloseShortSummary() => Tap(MapPageLocators.ShortSummaryCloseButton(_platform));
}