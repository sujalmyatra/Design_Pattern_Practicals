using Practical_22.Domain.Interfaces;

namespace Practical_22.Infrastructure.Logging;

public class FileLogger : ILoggerService
{
    private static readonly FileLogger _instance = new FileLogger();

    private readonly string _filePath;

    private FileLogger()
    {
        _filePath = "Logs.txt";
    }

    public static FileLogger Instance { get { return _instance; } }

    public void Log(string message)
    {
        string logMessage = $"{DateTime.Now} :: {message}\n";

        File.AppendAllText(_filePath, logMessage);
    }
}
