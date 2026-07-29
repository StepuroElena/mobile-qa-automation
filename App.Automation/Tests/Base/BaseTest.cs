using App.Automation.Config;
using App.Automation.Pages;
using App.Automation.Utils.Data;
using App.Automation.Utils.Logger;
using OpenQA.Selenium.Appium;

namespace App.Automation.Tests;

[Parallelizable(ParallelScope.Self)]
public class BaseTest
{
    protected AppiumDriver Driver { get; private set; } = null!;
    protected ITestLogger Logger { get; private set; } = null!;

    private static AppSettings _settings = null!;
    protected AppSettings Settings => _settings;
    private IDisposable? _testLogContext;
    
    protected string GeneratedEmail { get; private set; } = string.Empty;
    protected string GeneratedPassword { get; private set; } = string.Empty;
    protected string GeneratedFullName { get; private set; } = string.Empty;

    [OneTimeSetUp]
    public void GlobalSetup()
    {
        _settings = ConfigReader.Load();
        LoggerBootstrapper.Initialize(_settings.Logging);
    }

    [SetUp]
    public void SetUp()
    {
        Logger = new SerilogTestLogger();

        var testName = TestContext.CurrentContext.Test.Name;
        _testLogContext = TestContextLogger.PushTestContext(testName);

        Logger.Info($"=== Starting test: {testName} ===");

        Driver = DriverManager.GetDriver(_settings);

        GeneratedEmail = TestDataGenerator.GenerateEmail();
        GeneratedPassword = TestDataGenerator.GeneratePassword();
        GeneratedFullName = TestDataGenerator.GenerateFullName();
    }

    [TearDown]
    public void TearDown()
    {
        var result = TestContext.CurrentContext.Result.Outcome.Status;
        var testName = TestContext.CurrentContext.Test.Name;

        if (result == NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            Logger.Error($"=== Test FAILED: {testName} ===");
            CaptureScreenshotOnFailure(testName);
        }
        else
        {
            Logger.Info($"=== Test PASSED: {testName} ===");
        }

        DriverManager.QuitDriver();
        _testLogContext?.Dispose();
    }

    [OneTimeTearDown]
    public void GlobalTearDown()
    {
        LoggerBootstrapper.CloseAndFlush();
    }
    
    private void CaptureScreenshotOnFailure(string testName)
    {
        try
        {
            var screenshotsDir = Path.Combine(AppContext.BaseDirectory, "Screenshots");
            Directory.CreateDirectory(screenshotsDir);

            var fileName = $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            var filePath = Path.Combine(screenshotsDir, fileName);

            var screenshot = Driver.GetScreenshot();
            screenshot.SaveAsFile(filePath);

            Logger.Info($"Screenshot saved: {filePath}");
            TestContext.AddTestAttachment(filePath, "Failure screenshot");
        }
        catch (Exception ex)
        {
            Logger.Error("Failed to capture screenshot on test failure", ex);
        }
    }
}