using OpenQA.Selenium.Appium;

namespace App.Automation.Pages;

public interface IDriverFactory
{
    AppiumDriver CreateDriver();
}