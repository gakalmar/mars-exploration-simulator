namespace TerrainExplorer.Logger;

public class FileLogger : LoggerBase
{
    private readonly string _logFile;

    public FileLogger(string logFile)
    {
        _logFile = logFile;
        ClearLogFile();
    }
    
    protected override void LogMessage(string message, string type)
    {
        string workDir = AppDomain.CurrentDomain.BaseDirectory;
        string path = $"{workDir}/Output";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory($"{workDir}/Output");
        }
        var entry = CreateLogEntry(message, type);
        using var streamWriter = File.AppendText(_logFile);
        streamWriter.WriteLine(entry);
    }
    
    private static string CreateLogEntry(string message, string type) => $"[{DateTime.Now}] {type}: \n{message}";

    private void ClearLogFile()
    {
        string workDir = AppDomain.CurrentDomain.BaseDirectory;
        string path = $"{workDir}/Output";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory($"{workDir}/Output");
        }
        File.WriteAllText(_logFile, string.Empty);
    }
}