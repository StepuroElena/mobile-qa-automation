using OpenQA.Selenium.Appium;

namespace App.Automation.Pages;

public class WeatherDetailsPage : BasePage
{
    private readonly string _platform;

    public WeatherDetailsPage(AppiumDriver driver, int explicitWaitSeconds, string platform)
        : base(driver, explicitWaitSeconds)
    {
        PlatformHelper.EnsureSupportedPlatform(platform);
        _platform = platform;
    }

    public string GetCityTitle() => GetText(WeatherDetailsPageLocators.CityTitle(_platform));

    public string GetCurrentTemperature()
    {
        var candidates = Driver.FindElements(WeatherDetailsPageLocators.TemperatureCandidates(_platform));
        var largest = candidates.OrderByDescending(element => element.Size.Height).First();
        return largest.Text;
    }

    public string GetCurrentCondition() => GetText(WeatherDetailsPageLocators.CurrentCondition(_platform));

    public int GetDailyForecastRowCount()
    {
        return Driver.FindElements(WeatherDetailsPageLocators.DailyForecastRow(_platform)).Count;
    }

    public void TapBackButton() => Tap(WeatherDetailsPageLocators.BackButton(_platform));
    public bool IsDisplayed() => IsDisplayed(WeatherDetailsPageLocators.CityTitle(_platform));
}