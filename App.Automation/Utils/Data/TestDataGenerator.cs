using App.Automation.Utils.Logger;

namespace App.Automation.Utils.Data;

public static class TestDataGenerator
{
    private const string EmailPrefix = "qa_automation";
    private const string EmailDomain = "example.com";
    private static readonly ITestLogger _logger = new SerilogTestLogger();

    public static string GenerateEmail()
    {
        var timestamp = DateTime.UtcNow.ToString("yyyyMMdd_HHmmss");
        var randomSuffix = Guid.NewGuid().ToString("N")[..6];

        var email = $"{EmailPrefix}_{timestamp}_{randomSuffix}@{EmailDomain}";
        _logger.Info($"Generated email: {email}");
        return email;
    }

    public static string GeneratePassword()
    {
        var randomPart = Guid.NewGuid().ToString("N")[..8];
        var password = $"Qa{randomPart}1!";
        _logger.Info($"Generated password: {password}");
        return password;
    }

    public static string GenerateFullName()
    {
        var randomSuffix = Guid.NewGuid().ToString("N")[..4];
        var fullName = $"QA Tester {randomSuffix}";
        _logger.Info($"Generated full name: {fullName}");
        return fullName;
    }
}