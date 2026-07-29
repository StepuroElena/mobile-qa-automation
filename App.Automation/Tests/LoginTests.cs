using App.Automation.Utils.Logger.Report;

namespace App.Automation.Tests;


[TestFixture]
public class LoginTests :BaseTest
{
    [Test]
    [Description("Example")]
    public void LoginExample()
    {
        StepLogger.Step(Logger, "Open the login зpage", () =>
        {
            // TODO: implement navigation via LoginPage
        });

        StepLogger.Step(Logger, "Enter valid username", () =>
        {
            // TODO: implement via LoginPage.EnterUsername(...)
        });

        StepLogger.Step(Logger, "Enter valid password", () =>
        {
            // TODO: implement via LoginPage.EnterPassword(...)
        });

        StepLogger.Step(Logger, "Tap the login button", () =>
        {
            // TODO: implement via LoginPage.TapLoginButton()
        });

        Assert.Ignore("Test implementation Page Object (LoginPage).");
    }
}