namespace App.Automation.Pages;

public static class PlatformHelper
{
    public static bool IsAndroid(string platform) =>
        platform.Equals("Android", StringComparison.OrdinalIgnoreCase);

    public static bool IsIos(string platform) =>
        platform.Equals("iOS", StringComparison.OrdinalIgnoreCase);

    public static void EnsureSupportedPlatform(string platform)
    {
        if (!IsAndroid(platform) && !IsIos(platform))
        {
            throw new InvalidOperationException(
                $"Unsupported platform '{platform}'. Expected 'Android' or 'iOS'.");
        }
    }
}