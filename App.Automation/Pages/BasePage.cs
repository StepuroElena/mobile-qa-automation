using App.Automation.Utils.Logger;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;

namespace App.Automation.Pages;

public abstract class BasePage
{
    protected readonly AppiumDriver Driver;
    protected readonly int ExplicitWaitSeconds;
    protected readonly string Platform;
    private readonly WebDriverWait _wait;
    protected static readonly ITestLogger Logger = new SerilogTestLogger();

    protected BasePage(AppiumDriver driver)
    {
        Driver = driver;

        var settings = App.Automation.Config.ConfigReader.Load();
        Platform = settings.Platform;
        _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(settings.Execution.ExplicitWaitSeconds));
    }

    protected IWebElement FindVisible(By locator)
    {
        Logger.Info($"Looking for element: {locator}");
        return _wait.Until(d => d.FindElement(locator).Displayed ? d.FindElement(locator) : null!);
    }

    protected void Tap(By locator)
    {
        Logger.Info($"Tapping element: {locator}");
        FindVisible(locator).Click();
    }

    protected TPage Tap<TPage>(By locator) where TPage : BasePage
    {
        Logger.Info($"Tapping element: {locator}");
        FindVisible(locator).Click();
        return (TPage)Activator.CreateInstance(typeof(TPage), Driver, ExplicitWaitSeconds, Platform)!;
    }

    protected void TypeText(By locator, string text)
    {
        Logger.Info($"Typing text '{text}' into element: {locator}");
        var element = FindVisible(locator);
        element.Clear();
        element.SendKeys(text);
    }

    protected string GetText(By locator)
    {
        Logger.Info($"Getting text from element: {locator}");
        return FindVisible(locator).Text;
    }

    protected bool IsDisplayed(By locator)
    {
        Logger.Info($"Checking if element is displayed: {locator}");
        try
        {
            return FindVisible(locator).Displayed;
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }
}