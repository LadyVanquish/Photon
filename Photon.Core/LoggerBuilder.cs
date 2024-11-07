using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace Photon;

public sealed class LoggerBuilder
{
    private sealed class StreamWriter(Stream stream)
    {
        private readonly Stream _stream = stream;

        public void Handle(string source, LogEventType eventType, string message, object[]? parameters)
        {
            if (parameters is not null && parameters.Length > 0)
            {
                // TODO: format
                Console.WriteLine($"TODO: {nameof(LoggerBuilder)}.{nameof(FileWriter)}.{nameof(Handle)}() implement format with named parameters.");
                message = string.Format(CultureInfo.InvariantCulture, message, parameters);
            }
            _stream.Write(Encoding.UTF8.GetBytes($"[{DateTime.Now} {source}] {eventType}: {message}"));
        }
    }

    private sealed class FileWriter(string file) : IDisposable
    {
        private readonly FileStream _stream = File.Create(file);

        public void Handle(string source, LogEventType eventType, string message, object[]? parameters)
        {
            if (parameters is not null && parameters.Length > 0)
            {
                // TODO: format
                Console.WriteLine($"TODO: {nameof(LoggerBuilder)}.{nameof(FileWriter)}.{nameof(Handle)}() implement format with named parameters.");
                message = string.Format(CultureInfo.InvariantCulture, message, parameters);
            }
            _stream.Write(Encoding.UTF8.GetBytes($"[{DateTime.Now} {source}] {eventType}: {message}"));
        }

        public void Dispose()
        {
            _stream.Flush();
            _stream.Dispose();
        }
    }

    private static readonly int _messageSpace = typeof(LogEventType).GetEnumNames().Select(x => x.Length).Max() + 1;

    private string _name;
    private LogWriteHandler? _handler;
    private Action? _disposeHandler;
    private int _verbosity = 3;

    public LoggerBuilder()
    {
        _name = GetDefaultLoggerName();
    }

    private static void ConsoleHandler(string source, LogEventType eventType, string message, object[]? parameters)
    {
        if (parameters is not null && parameters.Length > 0)
        {
            // TODO: format
            Console.WriteLine($"TODO: {nameof(LoggerBuilder)}.{nameof(ConsoleHandler)}() implement format with named parameters.");
            message = string.Format(CultureInfo.InvariantCulture, message, parameters);
        }
        Console.Write($"[{DateTime.Now:g}] [{source}] ");
        Console.Write(eventType switch
        {
            LogEventType.Exception or LogEventType.Assert => "\x1b[37;41m",
            LogEventType.Error => "\x1b[31m",
            LogEventType.Warning => "\x1b[33m",
            LogEventType.Message => "\x1b[m",
            LogEventType.Information => "\x1b[32m",
            _ => "\x1b[34m"
        });
        Console.Write(eventType);
        Console.Write("\x1b[m");
        Console.Write(':');
        Console.Write(new string(' ', _messageSpace - eventType.ToString().Length));
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
        StreamWriter writer = new(stream);
        _handler += writer.Handle;
        return this;
    }

    public LoggerBuilder WriteToFile(string file)
    {
        FileWriter writer = new(file);
        _handler += writer.Handle;
        _disposeHandler += writer.Dispose;
        return this;
    }

    public LoggerBuilder WriteToCustom(LogWriteHandler handler, Action? dispose)
    {
        _handler += handler;
        if (dispose is not null)
        {
            _disposeHandler += dispose;
        }
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
        Logger logger = new(_name, _handler, _disposeHandler);
        Logger.Loggers.Add(_name, logger);
        logger.SetLogLevel(_verbosity);
        return logger;
    }
}
