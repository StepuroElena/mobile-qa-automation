using App.Automation.Pages;
using App.Automation.Utils.Logger.Report;

namespace App.Automation.Tests;

[TestFixture]
public class LoginTests : BaseTest
{
    [Test]
    [Description("Verifies that login fails with invalid credentials and an error message is shown")]
    public void Login_InvalidCredentials_ShowsError()
    {
        LoginPage loginPage = null!;

        StepLogger.Step(Logger, "Open the login screen", () =>
        {
            loginPage = new LoginPage(Driver, Settings.Execution.ExplicitWaitSeconds, Settings.Platform);
        });

        StepLogger.Step(Logger, "Enter an invalid email and password", () =>
        {
            loginPage.EnterEmail(GeneratedEmail);
            loginPage.EnterPassword(GeneratedPassword);
        });

        StepLogger.Step(Logger, "Tap the 'Login' button", () =>
        {
            loginPage.TapLoginButton();
        });

        StepLogger.Step(Logger, "Verify an error message is displayed on the login screen", () =>
        {
            var errorMessage = loginPage.GetErrorMessage();
            Assert.That(errorMessage, Is.Not.Empty, "Expected an error message to be shown for invalid credentials.");
            Assert.That(loginPage.IsDisplayed(), Is.True, "Expected to remain on the login screen after a failed login attempt.");
            Assert.That(errorMessage, Is.EqualTo("Invalid email or password."), "Expected the exact error message for invalid credentials.");
        });
    }
    
    [Test]
    [Description("Registers a new user, logs in, opens the map via Get Started, navigates back, and logs out successfully")]
    public void Logout_AfterLogin_ReturnsToLoginScreen()
    {
        LoginPage loginPage = null!;
        RegistrationPage registrationPage = null!;
        HomePage homePage = null!;
        MapPage mapPage = null!;
        SettingsPage settingsPage = null!;

        StepLogger.Step(Logger, "Open the login screen", () =>
        {
            loginPage = new LoginPage(Driver, Settings.Execution.ExplicitWaitSeconds, Settings.Platform);
        });

        StepLogger.Step(Logger, "Tap the 'Register' link to open the registration screen", () =>
        {
            loginPage.TapRegisterLink();
            registrationPage = new RegistrationPage(Driver, Settings.Execution.ExplicitWaitSeconds, Settings.Platform);
        });

        StepLogger.Step(Logger, $"Register a new user '{GeneratedEmail}'", () =>
        {
            registrationPage.Register(GeneratedFullName, GeneratedEmail, GeneratedPassword);
        });

        StepLogger.Step(Logger, "Log in with the newly registered credentials", () =>
        {
            loginPage.Login(GeneratedEmail, GeneratedPassword);
        });

        StepLogger.Step(Logger, "Verify the login succeeded (no longer on the login screen)", () =>
        {
            Assert.That(loginPage.IsDisplayed(), Is.False, "Expected to leave the login screen after a successful login.");
        });

        StepLogger.Step(Logger, "Tap 'Get Started' on the Home screen", () =>
        {
            homePage = new HomePage(Driver, Settings.Execution.ExplicitWaitSeconds, Settings.Platform);
            homePage.TapGetStarted();
        });

        StepLogger.Step(Logger, "Verify the map screen is displayed", () =>
        {
            mapPage = new MapPage(Driver, Settings.Execution.ExplicitWaitSeconds, Settings.Platform);
            Assert.That(mapPage.IsDisplayed(), Is.True, "Expected the map screen (search bar) to be displayed after tapping Get Started.");
        });

        StepLogger.Step(Logger, "Navigate back to the Home screen", () =>
        {
            mapPage.TapBackButton();
        });

        StepLogger.Step(Logger, "Verify the Home screen is displayed again", () =>
        {
            Assert.That(homePage.IsDisplayed(), Is.True, "Expected to be back on the Home screen after navigating back.");
        });

        StepLogger.Step(Logger, "Open Settings via the gear icon", () =>
        {
            homePage.TapSettingsIcon();
            settingsPage = new SettingsPage(Driver, Settings.Execution.ExplicitWaitSeconds, Settings.Platform);
        });

        StepLogger.Step(Logger, "Verify the Settings screen is displayed with the correct logged-in user", () =>
        {
            Assert.That(settingsPage.IsDisplayed(), Is.True, "Expected the Settings screen (Logout button) to be displayed.");
            Assert.That(settingsPage.GetLoggedInAsText(), Does.Contain(GeneratedFullName),
                "Expected the 'Logged in as' label to reflect the currently logged-in user.");
        });

        StepLogger.Step(Logger, "Tap the 'Logout' button", () =>
        {
            settingsPage.TapLogout();
        });

        StepLogger.Step(Logger, "Verify the user is returned to the login screen after logout", () =>
        {
            Assert.That(loginPage.IsDisplayed(), Is.True, "Expected to be back on the login screen after logout.");
        });
    }
}