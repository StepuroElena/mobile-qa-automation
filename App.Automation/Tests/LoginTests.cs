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
        var loginPage = new LoginPage(Driver);

        StepLogger.Step(Logger, "Enter an invalid email and password, then tap Login");
        loginPage.EnterEmail(GeneratedEmail);
        loginPage.EnterPassword(GeneratedPassword);
        loginPage.TapLoginButton();

        StepLogger.Step(Logger, "Verify the exact error message is displayed and the user remains on the login screen");
        var errorMessage = loginPage.GetErrorMessage();
        Assert.That(errorMessage, Is.EqualTo("Invalid email or password."), "Expected the exact error message for invalid credentials.");
        Assert.That(loginPage.IsDisplayed(), Is.True, "Expected to remain on the login screen after a failed login attempt.");
    }

    [Test]
    [Description("Registers a new user, logs in, opens the map via Get Started, navigates back, and logs out successfully")]
    public void Logout_AfterLogin_ReturnsToLoginScreen()
    {
        var loginPage = new LoginPage(Driver);

        StepLogger.Step(Logger, $"Tap the 'Register' link and register a new user '{GeneratedEmail}'");
        loginPage.TapRegisterLink().Register(GeneratedFullName, GeneratedEmail, GeneratedPassword);

        StepLogger.Step(Logger, "Log in with the newly registered credentials");
        var homePage = loginPage.Login(GeneratedEmail, GeneratedPassword);
        Assert.That(loginPage.IsDisplayed(), Is.False, "Expected to leave the login screen after a successful login.");

        StepLogger.Step(Logger, "Tap 'Get Started', verify the map screen, then navigate back to Home");
        var mapPage = homePage.TapGetStarted();
        Assert.That(mapPage.IsDisplayed(), Is.True, "Expected the map screen to be displayed after tapping Get Started.");
        homePage = mapPage.TapBackButton();
        Assert.That(homePage.IsDisplayed(), Is.True, "Expected to be back on the Home screen after navigating back.");

        StepLogger.Step(Logger, "Open Settings and verify the correct logged-in user");
        var settingsPage = homePage.TapSettingsIcon();
        Assert.That(settingsPage.IsDisplayed(), Is.True, "Expected the Settings screen to be displayed.");
        Assert.That(settingsPage.GetLoggedInAsText(), Does.Contain(GeneratedFullName),
            "Expected the 'Logged in as' label to reflect the currently logged-in user.");

        StepLogger.Step(Logger, "Tap the 'Logout' button");
        settingsPage.TapLogout();

        StepLogger.Step(Logger, "Verify the user is returned to the login screen after logout");
        Assert.That(loginPage.IsDisplayed(), Is.True, "Expected to be back on the login screen after logout.");
    }
}