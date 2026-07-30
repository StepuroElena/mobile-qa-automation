using OpenQA.Selenium.Appium;

namespace App.Automation.Pages;

public class WeatherDetailsPage : BasePage
{
    private readonly string _platform;

    public WeatherDetailsPage(AppiumDriver driver) : base(driver)
    {
        PlatformHelper.EnsureSupportedPlatform(Platform);
        _platform = Platform;
    }

    public string GetCityTitle()
    {
        Logger.Info("Getting city title");
        return GetText(WeatherDetailsPageLocators.CityTitle(_platform));
    }

    public string GetCurrentTemperature()
    {
        Logger.Info("Getting current temperature (largest text on screen)");
        var candidates = Driver.FindElements(WeatherDetailsPageLocators.TemperatureCandidates(_platform));
        var largest = candidates.OrderByDescending(element => element.Size.Height).First();
        return largest.Text;
    }

    public string GetCurrentCondition()
    {
        Logger.Info("Getting current condition");
        return GetText(WeatherDetailsPageLocators.CurrentCondition(_platform));
    }

    public int GetDailyForecastRowCount()
    {
        Logger.Info("Counting daily forecast rows");
        return Driver.FindElements(WeatherDetailsPageLocators.DailyForecastRow(_platform)).Count;
    }

    public MapPage TapBackButton()
    {
        Logger.Info("Tapping back button, navigating to MapPage");
        return Tap<MapPage>(WeatherDetailsPageLocators.BackButton(_platform));
    }

    public bool IsDisplayed()
    {
        Logger.Info("Checking if WeatherDetailsPage is displayed");
        return base.IsDisplayed(WeatherDetailsPageLocators.CityTitle(_platform));
    }
}