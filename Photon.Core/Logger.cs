﻿using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

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

public sealed class Logger : IDisposable
{
    private readonly struct InternalScopedTrace : IDisposable
    {
        private readonly Logger _logger;
        private readonly string _message;

        public InternalScopedTrace(Logger logger, string message)
        {
            lock (logger._instanceLock)
            {
                _logger = logger;
                _message = message;
                _logger.Output(LogEventType.Scope, $"Entering: {_message}");
                ++_logger.IndentLevel;
            }
        }

        public void Dispose()
        {
            lock (_logger._instanceLock)
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

    private static readonly LogEventType[] _verbosityLevels =
    [
        LogEventType.None,
        LogEventType.Exception | LogEventType.Assert | LogEventType.Error,
        LogEventType.Exception | LogEventType.Assert | LogEventType.Error | LogEventType.Warning,
        LogEventType.Exception | LogEventType.Assert | LogEventType.Error | LogEventType.Warning | LogEventType.Message,
        LogEventType.Exception | LogEventType.Assert | LogEventType.Error | LogEventType.Warning | LogEventType.Message | LogEventType.Information,
        LogEventType.Exception | LogEventType.Assert | LogEventType.Error | LogEventType.Warning | LogEventType.Message | LogEventType.Information | LogEventType.Note,
        (LogEventType)255,
        LogEventType.All
    ];

    private static readonly Logger _nullLogger = new("NullLogger", null, null);
    private static readonly object _lock = new();

    internal static Dictionary<string, Logger> Loggers { get; } = [];

#if DEBUG
    public static Logger WindowsLogger { get; } = new LoggerBuilder().WithName("Photon.Windows").WithVerbosityLevel(7).WriteToConsole().Build();
#endif

    private readonly object _instanceLock = new();
    private readonly string _name;
    private readonly LogWriteHandler? _handler;
    private readonly Action? _dispose;

    internal LogEventType LogMask { get; private set; }

    public int IndentLevel { get; private set; }

    internal Logger(string name, LogWriteHandler? handler, Action? dispose)
    {
        _name = name;
        _handler = handler;
        _dispose = dispose;
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
        _handler?.Invoke(_name, eventType, string.Concat(new string(' ', IndentLevel * 4), message), parameters);
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

    private const string _hex = "0123456789ABCDEF";

    private string GetLogDataOutput(string message, ReadOnlySpan<byte> data)
    {
        Span<char> output = stackalloc char[128];
        output.Fill(' ');
        StringBuilder sb = new();
        sb.AppendLine(CultureInfo.InvariantCulture, $"dumping memory for object {message} ({data.Length} bytes):");
        int bytes;
        int asciiOutput = 2;
        for (bytes = 0; bytes < data.Length; ++bytes, asciiOutput += 2)
        {
            if (bytes % 16 == 0)
            {
                sb.Append(CultureInfo.InvariantCulture, $"{bytes:X8}: ");
            }
            byte b = data[bytes];
            output[asciiOutput] = _hex[b >> 4];
            output[asciiOutput + 1] = _hex[b & 0x0F];
            output[(asciiOutput / 2) + 40] = !char.IsControl((char)b) ? (char)b : '.';
            if (bytes > 0)
            {
                if (((bytes + 1) % 16) == 0)
                {
                    sb.AppendLine(output[..((asciiOutput / 2) + 40 + 1)].ToString());
                    output.Fill(' ');
                    asciiOutput = 0;
                }
                else if (((bytes + 1) % 4) == 0)
                {
                    ++asciiOutput;
                }
            }
        }
        if ((bytes % 16) != 0)
        {
            sb.AppendLine(output[..((asciiOutput / 2) + 40 + 1)].ToString());
        }
        return sb.ToString();
    }

    public void LogData(LogEventType eventType, string message, ReadOnlySpan<byte> data)
    {
        if ((LogMask & eventType) != eventType)
        {
            return;
        }
        Output(eventType, GetLogDataOutput(message, data));
    }

    public void LogData(LogEventType eventType, [InterpolatedStringHandlerArgument("", "eventType")] ref LoggingInterpolatedStringHandler stringHandler, ReadOnlySpan<byte> data)
    {
        if ((LogMask & eventType) != eventType)
        {
            return;
        }
        Output(eventType, GetLogDataOutput(stringHandler.ToString(), data));
    }

    public void LogException(Exception ex)
    {
        if ((LogMask & LogEventType.Exception) != LogEventType.Exception)
        {
            return;
        }
        Output(LogEventType.Exception, ex.ToString());
    }

    public void Dispose()
    {
        _dispose?.Invoke();
    }

    public static void Close()
    {
        lock (_lock)
        {
            foreach (Logger logger in Loggers.Values)
            {
                logger.Dispose();
            }
            Loggers.Clear();
        }
    }
}
