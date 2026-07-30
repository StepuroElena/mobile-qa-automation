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

    protected BasePage(AppiumDriver driver, int explicitWaitSeconds, string platform)
    {
        Driver = driver;
        ExplicitWaitSeconds = explicitWaitSeconds;
        Platform = platform;
        _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(explicitWaitSeconds));
    }

    protected IWebElement FindVisible(By locator)
    {
        return _wait.Until(d => d.FindElement(locator).Displayed ? d.FindElement(locator) : null!);
    }

    protected void Tap(By locator)
    {
        FindVisible(locator).Click();
    }
    
    protected TPage Tap<TPage>(By locator) where TPage : BasePage
    {
        FindVisible(locator).Click();
        return (TPage)Activator.CreateInstance(typeof(TPage), Driver, ExplicitWaitSeconds, Platform)!;
    }

    protected void TypeText(By locator, string text)
    {
        var element = FindVisible(locator);
        element.Clear();
        element.SendKeys(text);
    }

    protected string GetText(By locator)
    {
        return FindVisible(locator).Text;
    }

    protected bool IsDisplayed(By locator)
    {
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