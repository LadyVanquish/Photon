using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Photon;

public sealed class LoggerBuilder
{
    private sealed class FileWriter
    {
        private readonly Stream _stream;

        public FileWriter(Stream stream)
        {
            _stream = stream;
        }

        public void Handle(string source, LogEventType eventType, string message, string[]? parameters)
        {
            if (parameters is not null && parameters.Length > 0)
            {
                // TODO: format
                Console.WriteLine($"TODO: {nameof(LoggerBuilder)}.{nameof(FileWriter)}.{nameof(Handle)}() needs to implement format.");
            }
            _stream.Write(Encoding.UTF8.GetBytes($"[{DateTime.Now} {source}] {eventType}: {message}"));
        }
    }

    private string _name;
    private LogWriteHandler? _handler;
    private int _verbosity = 3;

    public LoggerBuilder()
    {
        _name = GetDefaultLoggerName();
    }

    private static void ConsoleHandler(string source, LogEventType eventType, string message, string[]? parameters)
    {
        if (parameters is not null && parameters.Length > 0)
        {
            // TODO: format
            Console.WriteLine($"TODO: {nameof(LoggerBuilder)}.{nameof(ConsoleHandler)}() needs to implement format.");
        }
        Console.Write($"[{DateTime.Now:g}] [{source}] ");
        ConsoleColor foreground = Console.ForegroundColor;
        ConsoleColor background = Console.BackgroundColor;
        Console.ForegroundColor = eventType switch
        {
            LogEventType.Critical => ConsoleColor.White,
            LogEventType.Error => ConsoleColor.DarkRed,
            LogEventType.Warning => ConsoleColor.Yellow,
            LogEventType.Information => ConsoleColor.Green,
            _ => ConsoleColor.Blue
        };
        Console.BackgroundColor = eventType switch
        {
            LogEventType.Critical => ConsoleColor.DarkRed,
            _ => background
        };
        Console.Write($"{eventType}");
        Console.ForegroundColor = foreground;
        Console.BackgroundColor = background;
        Console.Write(": ");
        Console.WriteLine(message);
    }

    private static string? GetAssemblyTitle(Assembly? assembly)
    {
        if (assembly is null)
        {
            return null;
        }

        AssemblyTitleAttribute? attribute = assembly.GetCustomAttribute<AssemblyTitleAttribute>();
        if (attribute is null)
        {
            return null;
        }

        return attribute.Title;
    }

    internal static string GetDefaultLoggerName()
    {
        string? assemblyTitle = GetAssemblyTitle(Assembly.GetEntryAssembly());
        if (!string.IsNullOrEmpty(assemblyTitle))
        {
            return assemblyTitle!;
        }

        return "Photon";
    }

    public LoggerBuilder WriteToConsole()
    {
        _handler += ConsoleHandler;
        return this;
    }

    public LoggerBuilder WriteToStream(Stream stream)
    {
        FileWriter writer = new(stream);
        _handler += writer.Handle;
        return this;
    }

    public LoggerBuilder WriteToCustom(LogWriteHandler handler)
    {
        _handler += handler;
        return this;
    }

    public LoggerBuilder WithVerbosityLevel(int verbosityLevel)
    {
        _verbosity = verbosityLevel;
        return this;
    }

    public LoggerBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public Logger Build()
    {
        Debug.Assert(!Logger.Loggers.TryGetValue(_name, out _));
        Logger logger = new(_name);
        Logger.Loggers.Add(_name, logger);
        logger.WriteHandler = _handler;
        logger.SetLogLevel(_verbosity);
        return logger;
    }
}
