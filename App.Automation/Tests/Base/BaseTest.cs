using App.Automation.Config;
using App.Automation.Pages;
using App.Automation.Utils.Logger;
using OpenQA.Selenium.Appium;

namespace App.Automation.Tests;

[Parallelizable(ParallelScope.Self)]
public class BaseTest
{
    protected AppiumDriver Driver { get; private set; } = null!;
    protected ITestLogger Logger { get; private set; } = null!;

    private static AppSettings _settings = null!;
    private IDisposable? _testLogContext;

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
    }

    [TearDown]
    public void TearDown()
    {
        var result = TestContext.CurrentContext.Result.Outcome.Status;
        var testName = TestContext.CurrentContext.Test.Name;

        if (result == NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            Logger.Error($"=== Test FAILED: {testName} ===");
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
}