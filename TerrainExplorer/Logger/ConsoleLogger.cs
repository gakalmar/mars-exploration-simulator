namespace TerrainExplorer.Logger;

public class ConsoleLogger : LoggerBase
{
    protected override void LogMessage(string message, string type)
    {
        var entry = CreateLogEntry(message, type);
        Console.WriteLine(entry);
    }
    
    private static string CreateLogEntry(string message, string type) => $"[{DateTime.Now}] {type}: {message}";
}