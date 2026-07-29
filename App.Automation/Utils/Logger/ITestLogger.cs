namespace App.Automation.Utils.Logger;

public interface ITestLogger
{
    void Info(string message);
    void Debug(string message);
    void Warn(string message);
    void Error(string message, Exception? exception = null);
}