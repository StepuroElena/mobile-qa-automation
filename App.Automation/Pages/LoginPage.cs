using App.Automation.Pages.Locators;
using OpenQA.Selenium.Appium;

namespace App.Automation.Pages;

public class LoginPage : BasePage
{
    private readonly string _platform;

    public LoginPage(AppiumDriver driver, int explicitWaitSeconds, string platform) : base(driver, explicitWaitSeconds, platform)
    {
        PlatformHelper.EnsureSupportedPlatform(platform);
        _platform = platform;
    }

    public void EnterEmail(string email) => TypeText(LoginPageLocators.EmailField(_platform), email);

    public void EnterPassword(string password) => TypeText(LoginPageLocators.PasswordField(_platform), password);

    public LoginPage TapLoginButton() => Tap<LoginPage>(LoginPageLocators.LoginButton(_platform));

    public RegistrationPage TapRegisterLink() => Tap<RegistrationPage>(LoginPageLocators.RegisterLink(_platform));

    public string GetErrorMessage() => GetText(LoginPageLocators.ErrorMessage(_platform));

    public bool IsDisplayed() => IsDisplayed(LoginPageLocators.LoginButton(_platform));

    public HomePage Login(string email, string password)
    {
        EnterEmail(email);
        EnterPassword(password);
        TapLoginButton();
        return new HomePage(Driver, ExplicitWaitSeconds, Platform);
    }
}