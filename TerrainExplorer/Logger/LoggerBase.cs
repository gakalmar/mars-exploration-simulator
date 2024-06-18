namespace TerrainExplorer.Logger;

public abstract class LoggerBase : ILogger
{
    protected abstract void LogMessage(string message, string type);
    

    public void LogInfo(string message)
    {
        LogMessage(message, "INFO");
    }

    public void LogError(string message)
    {
        LogMessage(message, "ERROR");
    }
}