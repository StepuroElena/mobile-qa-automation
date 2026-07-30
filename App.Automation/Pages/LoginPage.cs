using App.Automation.Pages.Locators;
using OpenQA.Selenium.Appium;

namespace App.Automation.Pages;

public class LoginPage : BasePage
{
    private readonly string _platform;

    public LoginPage(AppiumDriver driver) : base(driver)
    {
        PlatformHelper.EnsureSupportedPlatform(Platform);
        _platform = Platform;
    }

    public void EnterEmail(string email)
    {
        Logger.Info($"Entering email: {email}");
        TypeText(LoginPageLocators.EmailField(_platform), email);
    }

    public void EnterPassword(string password)
    {
        Logger.Info("Entering password");
        TypeText(LoginPageLocators.PasswordField(_platform), password);
    }

    public HomePage TapLoginButton()
    {
        Logger.Info("Tapping 'Login' button, navigating to HomePage");
        return Tap<HomePage>(LoginPageLocators.LoginButton(_platform));
    }

    public RegistrationPage TapRegisterLink()
    {
        Logger.Info("Tapping 'Register' link, navigating to RegistrationPage");
        return Tap<RegistrationPage>(LoginPageLocators.RegisterLink(_platform));
    }

    public string GetErrorMessage()
    {
        Logger.Info("Getting login error message");
        return GetText(LoginPageLocators.ErrorMessage(_platform));
    }

    public bool IsDisplayed()
    {
        Logger.Info("Checking if LoginPage is displayed");
        return base.IsDisplayed(LoginPageLocators.LoginButton(_platform));
    }

    public HomePage Login(string email, string password)
    {
        Logger.Info($"Logging in with email: {email}");
        EnterEmail(email);
        EnterPassword(password);
        return TapLoginButton();
    }
}