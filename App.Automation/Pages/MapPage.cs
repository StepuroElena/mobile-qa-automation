using OpenQA.Selenium.Appium;

namespace App.Automation.Pages;

public class MapPage : BasePage
{
    private readonly string _platform;

    public MapPage(AppiumDriver driver) : base(driver)
    {
        PlatformHelper.EnsureSupportedPlatform(Platform);
        _platform = Platform;
    }

    public void EnterSearchQuery(string cityName)
    {
        Logger.Info($"Entering search query: {cityName}");
        TypeText(MapPageLocators.SearchField(_platform), cityName);
    }

    public WeatherDetailsPage TapFirstSearchResult(string cityName)
    {
        Logger.Info($"Tapping first search result for: {cityName}, navigating to WeatherDetailsPage");
        return Tap<WeatherDetailsPage>(MapPageLocators.FirstSearchResult(_platform, cityName));
    }

    public HomePage TapBackButton()
    {
        Logger.Info("Tapping back button, navigating to HomePage");
        return Tap<HomePage>(MapPageLocators.BackButton(_platform));
    }

    public bool IsDisplayed()
    {
        Logger.Info("Checking if MapPage is displayed");
        return base.IsDisplayed(MapPageLocators.SearchField(_platform));
    }

    public void TapMapNearCenter()
    {
        var windowSize = Driver.Manage().Window.Size;

        var x = (int)(windowSize.Width * 0.35);
        var y = (int)(windowSize.Height * 0.40);

        Logger.Info($"Tapping map near center at coordinates ({x}, {y})");

        var args = new Dictionary<string, object>
        {
            { "x", x },
            { "y", y }
        };

        Driver.ExecuteScript("mobile: clickGesture", args);
    }

    public string GetShortSummaryCity()
    {
        Logger.Info("Getting short summary city");
        return GetText(MapPageLocators.ShortSummaryCityLabel(_platform));
    }

    public string GetShortSummaryTemperature()
    {
        Logger.Info("Getting short summary temperature");
        return GetText(MapPageLocators.ShortSummaryTemperature(_platform));
    }

    public string GetShortSummaryCondition()
    {
        Logger.Info("Getting short summary condition");
        return GetText(MapPageLocators.ShortSummaryConditionLabel(_platform));
    }

    public bool IsShortSummaryDisplayed()
    {
        Logger.Info("Checking if short summary popup is displayed");
        return base.IsDisplayed(MapPageLocators.ShortSummaryCityLabel(_platform));
    }

    public void CloseShortSummary()
    {
        Logger.Info("Closing short summary popup");
        Tap(MapPageLocators.ShortSummaryCloseButton(_platform));
    }
}