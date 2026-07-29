using App.Automation.Pages;
using App.Automation.Utils.Logger.Report;

namespace App.Automation.Tests;

[TestFixture]
public class MapWeatherTests : BaseTest
{
    private const string SearchCity = "Almada";

    [Test]
    [Description(
        "Registers a user, logs in, searches a city on the map, and verifies the full weather details screen is displayed")]
    public void MapSearch_SelectCity_ShowsWeatherDetails()
    {
        LoginPage loginPage = null!;
        RegistrationPage registrationPage = null!;
        HomePage homePage = null!;
        MapPage mapPage = null!;
        WeatherDetailsPage weatherDetailsPage = null!;
        SettingsPage settingsPage = null!;

        StepLogger.Step(Logger, "Open the login screen",
            () => { loginPage = new LoginPage(Driver, Settings.Execution.ExplicitWaitSeconds, Settings.Platform); });

        StepLogger.Step(Logger, "Tap the 'Register' link to open the registration screen", () =>
        {
            loginPage.TapRegisterLink();
            registrationPage = new RegistrationPage(Driver, Settings.Execution.ExplicitWaitSeconds, Settings.Platform);
        });

        StepLogger.Step(Logger, $"Register a new user '{GeneratedEmail}'",
            () => { registrationPage.Register(GeneratedFullName, GeneratedEmail, GeneratedPassword); });

        StepLogger.Step(Logger, "Log in with the newly registered credentials",
            () => { loginPage.Login(GeneratedEmail, GeneratedPassword); });

        StepLogger.Step(Logger, "Verify the login succeeded",
            () =>
            {
                Assert.That(loginPage.IsDisplayed(), Is.False,
                    "Expected to leave the login screen after a successful login.");
            });

        StepLogger.Step(Logger, "Tap 'Get Started' to open the map", () =>
        {
            homePage = new HomePage(Driver, Settings.Execution.ExplicitWaitSeconds, Settings.Platform);
            homePage.TapGetStarted();
            mapPage = new MapPage(Driver, Settings.Execution.ExplicitWaitSeconds, Settings.Platform);
        });

        StepLogger.Step(Logger, $"Search for '{SearchCity}' and select it from the results", () =>
        {
            mapPage.EnterSearchQuery(SearchCity);
            mapPage.TapFirstSearchResult(SearchCity);
        });

        StepLogger.Step(Logger, "Verify the weather details screen is displayed", () =>
        {
            weatherDetailsPage =
                new WeatherDetailsPage(Driver, Settings.Execution.ExplicitWaitSeconds, Settings.Platform);
            Assert.That(weatherDetailsPage.IsDisplayed(), Is.True,
                "Expected the weather details screen to be displayed after selecting a search result.");
        });

        StepLogger.Step(Logger,
            "Verify the details show the searched city, a valid current temperature, and 5 daily forecast rows", () =>
            {
                var cityTitle = weatherDetailsPage.GetCityTitle();
                var temperature = weatherDetailsPage.GetCurrentTemperature();
                var dailyRowCount = weatherDetailsPage.GetDailyForecastRowCount();

                Assert.That(cityTitle, Does.Contain(SearchCity),
                    "Expected the details screen to display the searched city name.");
                Assert.That(temperature, Does.Match(@"-?\d+(\.\d+)?\s*°C"),
                    "Expected the largest-text temperature to be a valid value (e.g. '21.6 °C').");
                Assert.That(dailyRowCount, Is.EqualTo(5), "Expected exactly 5 daily forecast rows to be displayed.");
            });

        StepLogger.Step(Logger, "Navigate back to the map, then to Home", () =>
        {
            weatherDetailsPage.TapBackButton();
            mapPage.TapBackButton();
        });

        StepLogger.Step(Logger, "Verify the Home screen is displayed again",
            () => { Assert.That(homePage.IsDisplayed(), Is.True, "Expected to be back on the Home screen."); });

        StepLogger.Step(Logger, "Open Settings and log out", () =>
        {
            homePage.TapSettingsIcon();
            settingsPage = new SettingsPage(Driver, Settings.Execution.ExplicitWaitSeconds, Settings.Platform);
            settingsPage.TapLogout();
        });

        StepLogger.Step(Logger, "Verify the user is returned to the login screen after logout",
            () =>
            {
                Assert.That(loginPage.IsDisplayed(), Is.True, "Expected to be back on the login screen after logout.");
            });
    }

    [Test]
    [Description("Registers a user, logs in, taps a location on the map, and verifies the short weather summary popup is displayed")]
    public void MapTap_SelectLocation_ShowsShortWeatherSummary()
    {
        LoginPage loginPage = null!;
        RegistrationPage registrationPage = null!;
        HomePage homePage = null!;
        MapPage mapPage = null!;
        SettingsPage settingsPage = null!;

        StepLogger.Step(Logger, "Open the login screen",
            () => { loginPage = new LoginPage(Driver, Settings.Execution.ExplicitWaitSeconds, Settings.Platform); });

        StepLogger.Step(Logger, "Tap the 'Register' link to open the registration screen", () =>
        {
            loginPage.TapRegisterLink();
            registrationPage = new RegistrationPage(Driver, Settings.Execution.ExplicitWaitSeconds, Settings.Platform);
        });

        StepLogger.Step(Logger, $"Register a new user '{GeneratedEmail}'",
            () => { registrationPage.Register(GeneratedFullName, GeneratedEmail, GeneratedPassword); });

        StepLogger.Step(Logger, "Log in with the newly registered credentials",
            () => { loginPage.Login(GeneratedEmail, GeneratedPassword); });

        StepLogger.Step(Logger, "Verify the login succeeded",
            () =>
            {
                Assert.That(loginPage.IsDisplayed(), Is.False,
                    "Expected to leave the login screen after a successful login.");
            });

        StepLogger.Step(Logger, "Tap 'Get Started' to open the map", () =>
        {
            homePage = new HomePage(Driver, Settings.Execution.ExplicitWaitSeconds, Settings.Platform);
            homePage.TapGetStarted();
            mapPage = new MapPage(Driver, Settings.Execution.ExplicitWaitSeconds, Settings.Platform);
        });

        StepLogger.Step(Logger, "Tap a location near the center of the map", () => { mapPage.TapMapNearCenter(); });

        StepLogger.Step(Logger, "Verify the short weather summary popup is displayed",
            () =>
            {
                Assert.That(mapPage.IsShortSummaryDisplayed(), Is.True,
                    "Expected the short weather summary popup to appear after tapping a map location.");
            });

        StepLogger.Step(Logger, "Verify the summary shows non-empty city, temperature, and condition data", () =>
        {
            var city = mapPage.GetShortSummaryCity();
            var temperature = mapPage.GetShortSummaryTemperature();
            var condition = mapPage.GetShortSummaryCondition();

            Assert.That(city, Is.Not.Empty, "Expected a non-empty city name in the short summary.");
            Assert.That(temperature, Does.Match(@"-?\d+(\.\d+)?\s*°C"), "Expected a valid temperature value.");
            Assert.That(condition, Is.Not.Empty, "Expected a non-empty weather condition description.");
        });

        StepLogger.Step(Logger, "Close the short summary and navigate back to Home", () =>
        {
            mapPage.CloseShortSummary();
            mapPage.TapBackButton();
        });

        StepLogger.Step(Logger, "Verify the Home screen is displayed again",
            () => { Assert.That(homePage.IsDisplayed(), Is.True, "Expected to be back on the Home screen."); });

        StepLogger.Step(Logger, "Open Settings and log out", () =>
        {
            homePage.TapSettingsIcon();
            settingsPage = new SettingsPage(Driver, Settings.Execution.ExplicitWaitSeconds, Settings.Platform);
            settingsPage.TapLogout();
        });

        StepLogger.Step(Logger, "Verify the user is returned to the login screen after logout",
            () =>
            {
                Assert.That(loginPage.IsDisplayed(), Is.True, "Expected to be back on the login screen after logout.");
            });
    }
}