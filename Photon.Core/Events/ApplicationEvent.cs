namespace Photon.Events;

public sealed class WindowActivateEvent : PhotonEvent
{
    public string Title { get; }
    public bool IsActive { get; }

    public WindowActivateEvent(string title, bool isActive) : base(EventCategory.Application)
    {
        Title = title;
        IsActive = isActive;
    }

    public override string ToString()
    {
        return $"{nameof(WindowActivateEvent)}: {Title} {(IsActive ? "active" : "inactive")}";
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
