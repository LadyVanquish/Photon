using System.Runtime.CompilerServices;

namespace Photon;

[InterpolatedStringHandler]
public ref struct LoggingInterpolatedStringHandler
{
    private DefaultInterpolatedStringHandler _innerHandler;

    public LoggingInterpolatedStringHandler(int literalLength, int formattedCount, Logger logger, LogEventType eventType, out bool shouldAppend)
    {
        if ((logger.LogMask & eventType) != eventType)
        {
            _innerHandler = default;
            shouldAppend = false;
            return;
        }
        _innerHandler = new DefaultInterpolatedStringHandler(literalLength, formattedCount);
        shouldAppend = true;
    }

    public void AppendLiteral(string message)
    {
        _innerHandler.AppendLiteral(message);
    }

    public void AppendFormatted<T>(T message)
    {
        _innerHandler.AppendFormatted(message);
    }

    public override string ToString()
    {
        return _innerHandler.ToString();
    }

    public string ToStringAndClear()
    {
        return _innerHandler.ToStringAndClear();
    }
}
