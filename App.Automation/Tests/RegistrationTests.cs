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
        var loginPage = new LoginPage(Driver);

        StepLogger.Step(Logger, "Tap the 'Register' link to open the registration screen");
        var registrationPage = loginPage.TapRegisterLink();
        Assert.That(registrationPage.IsDisplayed(), Is.True, "Expected the registration screen to be displayed.");

        StepLogger.Step(Logger, $"Fill in registration details and register a new user '{GeneratedEmail}'");
        registrationPage.EnterFullName(GeneratedFullName);
        registrationPage.EnterEmail(GeneratedEmail);
        registrationPage.EnterPassword(GeneratedPassword);
        registrationPage.EnterConfirmPassword(GeneratedPassword);
        loginPage = registrationPage.TapRegisterButton();
        Assert.That(loginPage.IsDisplayed(), Is.True, "Expected to be back on the login screen after registration.");
        Assert.That(registrationPage.IsSuccessMessageDisplayed(), Is.True, "Expected the success toast to appear right after registration.");

        StepLogger.Step(Logger, "Log in with the newly registered credentials");
        loginPage.Login(GeneratedEmail, GeneratedPassword);
        Assert.That(loginPage.IsDisplayed(), Is.False, "Expected to leave the login screen after a successful login.");
    }

    [Test]
    [Description("Verifies that registering with an email that already exists shows an error message")]
    public void Registration_DuplicateEmail_ShowsError()
    {
        var loginPage = new LoginPage(Driver);

        StepLogger.Step(Logger, $"Register a new user '{GeneratedEmail}' for the first time");
        var registrationPage = loginPage.TapRegisterLink();
        registrationPage.Register(GeneratedFullName, GeneratedEmail, GeneratedPassword);
        Assert.That(loginPage.IsDisplayed(), Is.True, "Expected to be back on the login screen after the first registration.");

        StepLogger.Step(Logger, $"Attempt to register the same email '{GeneratedEmail}' again");
        registrationPage = loginPage.TapRegisterLink();
        registrationPage.Register(GeneratedFullName, GeneratedEmail, GeneratedPassword);

        StepLogger.Step(Logger, "Verify the exact duplicate-email error message is shown and the user remains on the registration screen");
        var errorMessage = registrationPage.GetErrorRegistrationMessage();
        Assert.That(errorMessage, Is.EqualTo("An account with this email already exists."),
            "Expected the exact error message for a duplicate email registration attempt.");
        Assert.That(registrationPage.IsDisplayed(), Is.True,
            "Expected to remain on the registration screen after a failed (duplicate) registration attempt.");
    }
    
    [Test]
    [Description("Verifies that registration fails with a validation error when password and confirm password don't match")]
    public void Registration_PasswordMismatch_ShowsError()
    {
        var loginPage = new LoginPage(Driver);

        StepLogger.Step(Logger, "Open registration screen and enter mismatched passwords");
        var registrationPage = loginPage.TapRegisterLink();
        registrationPage.EnterFullName(GeneratedFullName);
        registrationPage.EnterEmail(GeneratedEmail);
        registrationPage.EnterPassword(GeneratedPassword);
        registrationPage.EnterConfirmPassword(GeneratedPassword + "x");
        registrationPage.TapRegisterButtonWithoutNavigation();

        StepLogger.Step(Logger, "Verify a password mismatch error is shown and the user remains on the registration screen");
        var errorMessage = registrationPage.GetErrorPasswordMessage();
        Assert.That(errorMessage, Is.Not.Empty, "Expected a validation error for mismatched passwords.");
        Assert.That(errorMessage, Is.EqualTo("Passwords do not match."), "Expected a validation error for mismatched passwords.");
        Assert.That(registrationPage.IsDisplayed(), Is.True, "Expected to remain on the registration screen.");
    }
    
    [Test]
    [Description("Verifies the Register button is disabled until all required fields are filled")]
    public void Registration_EmptyFields_RegisterButtonDisabled()
    {
        var loginPage = new LoginPage(Driver);

        StepLogger.Step(Logger, "Open registration screen and verify Register button is disabled with empty fields");
        var registrationPage = loginPage.TapRegisterLink();
        Assert.That(registrationPage.IsRegisterButtonEnabled(), Is.False, "Expected Register button to be disabled when all fields are empty.");

        StepLogger.Step(Logger, "Fill in all fields and verify Register button becomes enabled");
        registrationPage.EnterFullName(GeneratedFullName);
        registrationPage.EnterEmail(GeneratedEmail);
        registrationPage.EnterPassword(GeneratedPassword);
        registrationPage.EnterConfirmPassword(GeneratedPassword);
        Assert.That(registrationPage.IsRegisterButtonEnabled(), Is.True, "Expected Register button to be enabled once all fields are filled.");
    }
}