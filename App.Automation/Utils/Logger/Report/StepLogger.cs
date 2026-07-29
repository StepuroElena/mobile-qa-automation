namespace App.Automation.Utils.Logger.Report;

public class StepLogger
{
    public static void Step(ITestLogger logger, string stepDescription, Action stepAction)
    {
        logger.Info($"STEP: {stepDescription}");

        try
        {
            stepAction();
            logger.Info($"STEP PASSED: {stepDescription}");
        }
        catch (Exception ex)
        {
            logger.Error($"STEP FAILED: {stepDescription}", ex);
            throw;
        }
    }
}