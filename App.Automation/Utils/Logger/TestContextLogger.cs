using Serilog.Context;

namespace App.Automation.Utils.Logger;

public class TestContextLogger
{
    public static IDisposable PushTestContext(string testName)
    {
        return LogContext.PushProperty("TestName", testName);
    }
}