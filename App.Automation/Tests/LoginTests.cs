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
}