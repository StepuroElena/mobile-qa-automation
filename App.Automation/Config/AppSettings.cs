namespace App.Automation.Config;

public class AppSettings
{
    public string Platform { get; set; } = string.Empty;
    public AndroidConfig Android { get; set; } = new();
    public IosConfig iOS { get; set; } = new();
    public ExecutionConfig Execution { get; set; } = new();
    public LoggingConfig Logging { get; set; } = new();
}

public class AndroidConfig
{
    public string PlatformName { get; set; } = string.Empty;
    public string AutomationName { get; set; } = string.Empty;
    public string DeviceName { get; set; } = string.Empty;
    public string AppPackage { get; set; } = string.Empty;
    public string AppActivity { get; set; } = string.Empty;
    public string AppPath { get; set; } = string.Empty;
    public bool FullReset { get; set; }

}

public class IosConfig
{
    public string PlatformName { get; set; } = string.Empty;
    public string AutomationName { get; set; } = string.Empty;
    public string DeviceName { get; set; } = string.Empty;
    public string PlatformVersion { get; set; } = string.Empty;
    public string BundleId { get; set; } = string.Empty;
    public bool FullReset { get; set; }

}

public class ExecutionConfig
{
    public string AppiumServerUrl { get; set; } = string.Empty;
    public int ImplicitWaitSeconds { get; set; }
    public int ExplicitWaitSeconds { get; set; }
    public int MaxParallelThreads { get; set; }
}

public class LoggingConfig
{
    public string LogFilePath { get; set; } = string.Empty;
}