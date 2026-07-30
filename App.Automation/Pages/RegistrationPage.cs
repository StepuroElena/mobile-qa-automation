using App.Automation.Pages.Locators;
using OpenQA.Selenium.Appium;

namespace App.Automation.Pages;

public class RegistrationPage : BasePage
{
    private readonly string _platform;

    public RegistrationPage(AppiumDriver driver, int explicitWaitSeconds, string platform)
        : base(driver, explicitWaitSeconds, platform)
    {
        PlatformHelper.EnsureSupportedPlatform(platform);
        _platform = platform;
    }

    public void EnterFullName(string fullName) => TypeText(RegistrationPageLocators.FullNameField(_platform), fullName);
    public void EnterEmail(string email) => TypeText(RegistrationPageLocators.EmailField(_platform), email);
    public void EnterPassword(string password) => TypeText(RegistrationPageLocators.PasswordField(_platform), password);

    public void EnterConfirmPassword(string password) =>
        TypeText(RegistrationPageLocators.ConfirmPasswordField(_platform), password);

    public LoginPage TapRegisterButton() => Tap<LoginPage>(RegistrationPageLocators.RegisterButton(_platform));
    public LoginPage TapLoginLink() => Tap<LoginPage>(RegistrationPageLocators.LoginLink(_platform));
    public bool IsDisplayed() => IsDisplayed(RegistrationPageLocators.RegisterButton(_platform));
    public string GetErrorMessage() => GetText(RegistrationPageLocators.ErrorMessage(_platform));

    public LoginPage Register(string fullName, string email, string password)
    {
        EnterFullName(fullName);
        EnterEmail(email);
        EnterPassword(password);
        EnterConfirmPassword(password);
        TapRegisterButton();
        return new LoginPage(Driver, ExplicitWaitSeconds, Platform);
    }
}