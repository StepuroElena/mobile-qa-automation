using OpenQA.Selenium.Appium;

namespace App.Automation.Pages;

public class WeatherDetailsPage : BasePage
{
    private readonly string _platform;

    public WeatherDetailsPage(AppiumDriver driver) : base(driver) => PlatformHelper.EnsureSupportedPlatform(Platform);

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

    public MapPage TapBackButton() => Tap<MapPage>(WeatherDetailsPageLocators.BackButton(_platform));
    public bool IsDisplayed() => IsDisplayed(WeatherDetailsPageLocators.CityTitle(_platform));
}