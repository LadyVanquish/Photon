namespace Photon.Events;

[Flags]
public enum EventCategory
{
    None = 0,
    Application = 1 << 0,
    Input = 1 << 1,
    Keyboard = 1 << 2,
    Mouse = 1 << 3,
    MouseButton = 1 << 4
}

public static class EventLoggerExtensions
{
    public static void Log(this Logger logger, PhotonEvent args)
    {
        logger.Log(LogEventType.Message, args.ToString()!);
    }
}

public abstract class PhotonEvent(EventCategory category)
{
    internal bool _handled;

    public EventCategory Category { get; } = category;

    public bool InCategory(EventCategory category)
    {
        return (Category & category) == category;
    }
}

public sealed class EventDispatcher(PhotonEvent args)
{
    private readonly PhotonEvent _event = args;

    public bool Dispatch<T>(Func<T, bool> func) where T : PhotonEvent
    {
        if (_event is T eventT)
        {
            _event._handled = func(eventT);
            return true;
        }
        return false;
    }
}
