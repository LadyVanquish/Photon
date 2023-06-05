namespace Photon.Events;

public sealed class WindowActivateEvent : PhotonEvent
{
    public string Title { get; }

    public WindowActivateEvent(string title) : base(EventCategory.Application)
    {
        Title = title;
    }

    public override string ToString()
    {
        return $"{nameof(WindowActivateEvent)}: {Title}";
    }
}

public sealed class WindowDeactivateEvent : PhotonEvent
{
    public string Title { get; }

    public WindowDeactivateEvent(string title) : base(EventCategory.Application)
    {
        Title = title;
    }

    public override string ToString()
    {
        return $"{nameof(WindowDeactivateEvent)}: {Title}";
    }
}

public sealed class WindowResizeEvent : PhotonEvent
{
    public uint Width { get; }
    public uint Height { get; }

    public WindowResizeEvent(uint width, uint height) : base(EventCategory.Application)
    {
        Width = width;
        Height = height;
    }

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

public sealed class ApplicationTickEvent : PhotonEvent
{
    public ApplicationTickEvent() : base(EventCategory.Application)
    {
    }

    public override string ToString()
    {
        return nameof(ApplicationTickEvent);
    }
}

public sealed class ApplicationUpdateEvent : PhotonEvent
{
    public ApplicationUpdateEvent() : base(EventCategory.Application)
    {
    }

    public override string ToString()
    {
        return nameof(ApplicationUpdateEvent);
    }
}

public sealed class ApplicationRenderEvent : PhotonEvent
{
    public ApplicationRenderEvent() : base(EventCategory.Application)
    {
    }

    public override string ToString()
    {
        return nameof(ApplicationRenderEvent);
    }
}
