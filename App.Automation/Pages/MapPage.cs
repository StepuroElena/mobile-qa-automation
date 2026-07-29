using OpenQA.Selenium.Appium;

namespace App.Automation.Pages;

public class MapPage : BasePage
{
    private readonly string _platform;

    public MapPage(AppiumDriver driver, int explicitWaitSeconds, string platform)
        : base(driver, explicitWaitSeconds)
    {
        PlatformHelper.EnsureSupportedPlatform(platform);
        _platform = platform;
    }

    public void EnterSearchQuery(string cityName) => TypeText(MapPageLocators.SearchField(_platform), cityName);
    public void TapFirstSearchResult(string cityName) => Tap(MapPageLocators.FirstSearchResult(_platform, cityName));
    public void TapBackButton() => Tap(MapPageLocators.BackButton(_platform));
    public bool IsDisplayed() => IsDisplayed(MapPageLocators.SearchField(_platform));

    public void TapMapNearCenter()
    {
        var windowSize = Driver.Manage().Window.Size;
        var x = windowSize.Width / 2;
        var y = (int)(windowSize.Height * 0.55); 

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