namespace App.Automation.Utils.Logger.Report;

public class StepLogger
{
    public static void Step(ITestLogger logger, string stepDescription)
    {
        logger.Info($"STEP: {stepDescription}");
    }
}