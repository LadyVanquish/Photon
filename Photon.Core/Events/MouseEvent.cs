namespace Photon.Events;

public sealed class MouseMovedEvent : PhotonEvent
{
    public float X { get; }
    public float Y { get; }

    public MouseMovedEvent(float x, float y) : base(EventCategory.Mouse | EventCategory.Input)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return $"{nameof(MouseMovedEvent)}: {X}, {Y}";
    }
}

public sealed class MouseScrolledEvent : PhotonEvent
{
    public float XOffset { get; }
    public float YOffset { get; }

    public MouseScrolledEvent(float xOffset, float yOffset) : base(EventCategory.Mouse | EventCategory.Input)
    {
        XOffset = xOffset;
        YOffset = yOffset;
    }

    public override string ToString()
    {
        return $"{nameof(MouseScrolledEvent)}: {XOffset}, {YOffset}";
    }
}

public abstract class MouseButtonEvent : PhotonEvent
{
    public int MouseButton { get; }

    public MouseButtonEvent(int mouseButton) : base(EventCategory.MouseButton | EventCategory.Mouse | EventCategory.Input)
    {
        MouseButton = mouseButton;
    }
}

public sealed class MouseButtonPressedEvent : MouseButtonEvent
{
    public MouseButtonPressedEvent(int mouseButton) : base(mouseButton)
    {
    }

    public override string ToString()
    {
        return $"{nameof(MouseButtonPressedEvent)}: {MouseButton}";
    }
}

public sealed class MouseButtonReleasedEvent : MouseButtonEvent
{
    public MouseButtonReleasedEvent(int mouseButton) : base(mouseButton)
    {
    }

    public override string ToString()
    {
        return $"{nameof(MouseButtonReleasedEvent)}: {MouseButton}";
    }
}
