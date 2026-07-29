using Serilog;

namespace App.Automation.Utils.Logger;

public class SerilogTestLogger:ITestLogger
{
    public void Info(string message) => Log.Information(message);

    public void Debug(string message) => Log.Debug(message);

    public void Warn(string message) => Log.Warning(message);

    public void Error(string message, Exception? exception = null)
    {
        if (exception is not null)
        {
            Log.Error(exception, message);
        }
        else
        {
            Log.Error(message);
        }
    }
}