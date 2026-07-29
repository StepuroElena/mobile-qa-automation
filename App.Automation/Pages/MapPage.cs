using App.Automation.Pages.Locators;
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
    public void TapFirstSearchResult() => Tap(MapPageLocators.FirstSearchResult(_platform));
    public string GetShortSummaryCity() => GetText(MapPageLocators.ShortSummaryCityLabel(_platform));
    public string GetShortSummaryTemperature() => GetText(MapPageLocators.ShortSummaryTemperature(_platform));
    public bool IsShortSummaryDisplayed() => IsDisplayed(MapPageLocators.ShortSummaryCityLabel(_platform));
    public void TapDetailsButton() => Tap(MapPageLocators.ShortSummaryDetailsButton(_platform));
    public void CloseShortSummary() => Tap(MapPageLocators.ShortSummaryCloseButton(_platform));
    public void TapBackButton() => Tap(MapPageLocators.BackButton(_platform));

    public bool IsDisplayed() => IsDisplayed(MapPageLocators.SearchField(_platform));
}