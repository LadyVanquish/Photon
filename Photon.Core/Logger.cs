﻿using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Photon;

[Flags]
public enum LogEventType : ushort
{
    None = 0,
    Exception = 1 << 0,
    Assert = 1 << 1,
    Error = 1 << 2,
    Warning = 1 << 3,
    Message = 1 << 4,
    Information = 1 << 5,
    Note = 1 << 8,
    Method = 1 << 9,
    Scope = 1 << 10,
    Constructor = 1 << 11,
    Property = 1 << 12,
    Data = 1 << 13,
    All = 0xFFFF
}

public delegate void LogWriteHandler(string source, LogEventType eventType, string message, object[]? parameters);

public sealed class Logger
{
    private readonly struct InternalScopedTrace : IDisposable
    {
        private readonly Logger _logger;
        private readonly string _message;

        public InternalScopedTrace(Logger logger, string message)
        {
            lock (logger._lock)
            {
                _logger = logger;
                _message = message;
                _logger.Output(LogEventType.Scope, $"Entering: {_message}");
                ++_logger.IndentLevel;
            }
        }

        public void Dispose()
        {
            lock (_logger._lock)
            {
                --_logger.IndentLevel;
                _logger.Output(LogEventType.Scope, $"Leaving: {_message}");
            }
        }
    }

    private readonly struct NullDisposable : IDisposable
    {
        public static readonly NullDisposable Default;

        public void Dispose()
        {
        }
    }

    private static readonly LogEventType[] _verbosityLevels = new[]
    {
        LogEventType.None,
        LogEventType.Exception | LogEventType.Assert | LogEventType.Error,
        LogEventType.Exception | LogEventType.Assert | LogEventType.Error | LogEventType.Warning,
        LogEventType.Exception | LogEventType.Assert | LogEventType.Error | LogEventType.Warning | LogEventType.Message,
        LogEventType.Exception | LogEventType.Assert | LogEventType.Error | LogEventType.Warning | LogEventType.Message | LogEventType.Information,
        LogEventType.Exception | LogEventType.Assert | LogEventType.Error | LogEventType.Warning | LogEventType.Message | LogEventType.Information | LogEventType.Note,
        (LogEventType)255,
        LogEventType.All
    };

    private static readonly Logger _nullLogger = new("NullLogger");

    internal static Dictionary<string, Logger> Loggers { get; } = new();

#if DEBUG
    public static Logger WindowsLogger { get; } = new LoggerBuilder().WithName("WindowsLogger").WithVerbosityLevel(7).WriteToConsole().Build();
#endif

    private readonly object _lock = new();
    private readonly string _name;

    internal LogEventType LogMask { get; private set; }

    public int IndentLevel { get; private set; }
    public LogWriteHandler? WriteHandler { get; internal set; }

    internal Logger(string name)
    {
        _name = name;
    }

    private static string GetCallingMethodInfo(string additionalInfo)
    {
        MethodBase method = new StackFrame(2).GetMethod()!;
        if (method.DeclaringType is null)
        {
            return $"{method.Name}({additionalInfo})";
        }
        return $"{method.DeclaringType.Name}.{method.Name}({additionalInfo})";
    }

    public static Logger GetLogger(string? name = null)
    {
        name ??= LoggerBuilder.GetDefaultLoggerName();
        Debug.Assert(Loggers.TryGetValue(name, out Logger? logger));
        if (logger is null)
        {
            Console.WriteLine($"Logger '{name}' not set up. Logging is disabled.");
            return _nullLogger;
        }
        return logger;
    }

    internal void SetLogLevel(int level)
    {
        LogMask = _verbosityLevels[level];
    }

    public void Output(LogEventType eventType, string message, object[]? parameters = null)
    {
        WriteHandler?.Invoke(_name, eventType, string.Concat(new string(' ', IndentLevel * 4), message), parameters);
    }

    public IDisposable LogMethod(string additionalInfo = "")
    {
        return (LogMask & LogEventType.Method) == LogEventType.Method ? new InternalScopedTrace(this, GetCallingMethodInfo(additionalInfo)) : NullDisposable.Default;
    }

    public void Log(LogEventType eventType, string message, params object[] parameters)
    {
        if ((LogMask & eventType) != eventType)
        {
            return;
        }
        Output(eventType, message, parameters);
    }

    public void Log(LogEventType eventType, [InterpolatedStringHandlerArgument("", "eventType")] ref LoggingInterpolatedStringHandler stringHandler)
    {
        if ((LogMask & eventType) != eventType)
        {
            return;
        }
        Output(eventType, stringHandler.ToString(), null);
    }
}