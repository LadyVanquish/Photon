namespace Photon.Events;

public sealed class WindowActivateEvent(string title) : PhotonEvent(EventCategory.Application)
{
    public string Title { get; } = title;

    public override string ToString()
    {
        return $"{nameof(WindowActivateEvent)}: {Title}";
    }
}

public sealed class WindowDeactivateEvent(string title) : PhotonEvent(EventCategory.Application)
{
    public string Title { get; } = title;

    public override string ToString()
    {
        return $"{nameof(WindowDeactivateEvent)}: {Title}";
    }
}

public sealed class WindowResizeEvent(uint width, uint height) : PhotonEvent(EventCategory.Application)
{
    public uint Width { get; } = width;
    public uint Height { get; } = height;

    public override string ToString()
    {
        return $"{nameof(WindowResizeEvent)}: {Width}, {Height}";
    }
}

public sealed class WindowCloseEvent : PhotonEvent
{
    public WindowCloseEvent() : base(EventCategory.Application)
    {
    }

    public override string ToString()
    {
        return nameof(WindowCloseEvent);
    }
}
