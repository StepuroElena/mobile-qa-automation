using App.Automation.Pages.Locators;
using OpenQA.Selenium.Appium;

namespace App.Automation.Pages;

public class RegistrationPage : BasePage
{
    private readonly string _platform;

    public RegistrationPage(AppiumDriver driver) : base(driver)
    {
        PlatformHelper.EnsureSupportedPlatform(Platform);
        _platform = Platform;
    }

    public void EnterFullName(string fullName)
    {
        Logger.Info($"Entering full name: {fullName}");
        TypeText(RegistrationPageLocators.FullNameField(_platform), fullName);
    }

    public void EnterEmail(string email)
    {
        Logger.Info($"Entering email: {email}");
        TypeText(RegistrationPageLocators.EmailField(_platform), email);
    }

    public void EnterPassword(string password)
    {
        Logger.Info("Entering password");
        TypeText(RegistrationPageLocators.PasswordField(_platform), password);
    }

    public void EnterConfirmPassword(string password)
    {
        Logger.Info("Entering confirm password");
        TypeText(RegistrationPageLocators.ConfirmPasswordField(_platform), password);
    }

    public LoginPage TapRegisterButton()
    {
        Logger.Info("Tapping 'Register' button, navigating to LoginPage");
        return Tap<LoginPage>(RegistrationPageLocators.RegisterButton(_platform));
    }

    public LoginPage TapLoginLink()
    {
        Logger.Info("Tapping 'Login' link, navigating to LoginPage");
        return Tap<LoginPage>(RegistrationPageLocators.LoginLink(_platform));
    }

    public bool IsDisplayed()
    {
        Logger.Info("Checking if RegistrationPage is displayed");
        return base.IsDisplayed(RegistrationPageLocators.RegisterButton(_platform));
    }

    public string GetErrorRegistrationMessage()
    {
        Logger.Info("Getting registration error message");
        return GetText(RegistrationPageLocators.ErrorRegistrationMessage(_platform));
    }
    
    public string GetErrorPasswordMessage()
    {
        Logger.Info("Getting password error message");
        return GetText(RegistrationPageLocators.ErrorPasswordMessage(_platform));
    }

    public LoginPage Register(string fullName, string email, string password)
    {
        Logger.Info($"Registering new user: {email}");
        EnterFullName(fullName);
        EnterEmail(email);
        EnterPassword(password);
        EnterConfirmPassword(password);
        return TapRegisterButton();
    }
    
    public bool IsRegisterButtonEnabled() => IsEnabled(RegistrationPageLocators.RegisterButton(_platform));

    public void TapRegisterButtonWithoutNavigation()
    {
        Logger.Info("Tapping 'Register' button (validation error expected, no navigation)");
        Tap(RegistrationPageLocators.RegisterButton(_platform));
    }
    
    public string GetSuccessMessage()
    {
        Logger.Info("Getting registration success message (toast-like element, short wait)");
        return GetTextShortWait(RegistrationPageLocators.SuccessMessage(_platform));
    }

    public bool IsSuccessMessageDisplayed()
    {
        Logger.Info("Checking if success message is displayed (toast-like element, short wait)");
        return IsDisplayedShortWait(RegistrationPageLocators.SuccessMessage(_platform));
    }
}