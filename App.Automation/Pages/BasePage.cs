using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;

namespace App.Automation.Pages;

public abstract class BasePage
{
    protected readonly AppiumDriver Driver;
    private readonly WebDriverWait _wait;

    protected BasePage(AppiumDriver driver, int explicitWaitSeconds)
    {
        Driver = driver;
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