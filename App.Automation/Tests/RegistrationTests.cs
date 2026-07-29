using App.Automation.Pages;
using App.Automation.Utils.Logger.Report;

namespace App.Automation.Tests;

[TestFixture]
public class RegistrationTests : BaseTest
{
    [Test]
    [Description("Verifies that a new user can register and then successfully log in with the same credentials")]
    public void Registration_NewUser_ThenLogin_Succeeds()
    {
        LoginPage loginPage = null!;
        RegistrationPage registrationPage = null!;

        StepLogger.Step(Logger, "Open the login screen", () =>
        {
            loginPage = new LoginPage(Driver, Settings.Execution.ExplicitWaitSeconds, Settings.Platform);
        });

        StepLogger.Step(Logger, "Tap the 'Register' link to open the registration screen", () =>
        {
            loginPage.TapRegisterLink();
            registrationPage = new RegistrationPage(Driver, Settings.Execution.ExplicitWaitSeconds, Settings.Platform);
            Assert.That(registrationPage.IsDisplayed(), Is.True, "Expected the registration screen to be displayed.");
        });

        StepLogger.Step(Logger, $"Enter full name '{GeneratedFullName}'", () =>
        {
            registrationPage.EnterFullName(GeneratedFullName);
        });

        StepLogger.Step(Logger, $"Enter a unique email '{GeneratedEmail}'", () =>
        {
            registrationPage.EnterEmail(GeneratedEmail);
        });

        StepLogger.Step(Logger, "Enter a generated password", () =>
        {
            registrationPage.EnterPassword(GeneratedPassword);
        });

        StepLogger.Step(Logger, "Enter the same password to confirm", () =>
        {
            registrationPage.EnterConfirmPassword(GeneratedPassword);
        });

        StepLogger.Step(Logger, "Tap the 'Register' button", () =>
        {
            registrationPage.TapRegisterButton();
        });

        StepLogger.Step(Logger, "Verify the user is returned to the login screen after successful registration", () =>
        {
            Assert.That(loginPage.IsDisplayed(), Is.True, "Expected to be back on the login screen after registration.");
        });

        StepLogger.Step(Logger, "Log in with the newly registered credentials", () =>
        {
            loginPage.Login(GeneratedEmail, GeneratedPassword);
        });

        StepLogger.Step(Logger, "Verify the login succeeds and the user is no longer on the login screen", () =>
        {
            Assert.That(loginPage.IsDisplayed(), Is.False, "Expected to leave the login screen after a successful login.");
        });
    }
}