namespace App.Automation.Utils.Data;

public static class TestDataGenerator
{
    private const string EmailPrefix = "qa_automation";
    private const string EmailDomain = "example.com";

    public static string GenerateEmail()
    {
        var timestamp = DateTime.UtcNow.ToString("yyyyMMdd_HHmmss");
        var randomSuffix = Guid.NewGuid().ToString("N")[..6];

        return $"{EmailPrefix}_{timestamp}_{randomSuffix}@{EmailDomain}";
    }

    public static string GeneratePassword()
    {
        var randomPart = Guid.NewGuid().ToString("N")[..8];
        return $"Qa{randomPart}1!";
    }

    public static string GenerateFullName()
    {
        var randomSuffix = Guid.NewGuid().ToString("N")[..4];
        return $"QA Tester {randomSuffix}";
    }
}