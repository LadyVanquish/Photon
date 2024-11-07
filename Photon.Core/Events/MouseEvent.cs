namespace Photon.Events;

public sealed class MouseMovedEvent(float x, float y) : PhotonEvent(EventCategory.Mouse | EventCategory.Input)
{
    public float X { get; } = x;
    public float Y { get; } = y;

    public override string ToString()
    {
        return $"{nameof(MouseMovedEvent)}: {X}, {Y}";
    }
}

public sealed class MouseScrolledEvent(float xOffset, float yOffset) : PhotonEvent(EventCategory.Mouse | EventCategory.Input)
{
    public float XOffset { get; } = xOffset;
    public float YOffset { get; } = yOffset;

    public override string ToString()
    {
        return $"{nameof(MouseScrolledEvent)}: {XOffset}, {YOffset}";
    }
}

public abstract class MouseButtonEvent(MouseButton mouseButton) : PhotonEvent(EventCategory.MouseButton | EventCategory.Mouse | EventCategory.Input)
{
    public MouseButton MouseButton { get; } = mouseButton;
}

public sealed class MouseButtonPressedEvent(MouseButton mouseButton) : MouseButtonEvent(mouseButton)
{
    public override string ToString()
    {
        return $"{nameof(MouseButtonPressedEvent)}: {MouseButton}";
    }
}

public sealed class MouseButtonReleasedEvent(MouseButton mouseButton) : MouseButtonEvent(mouseButton)
{
    public override string ToString()
    {
        return $"{nameof(MouseButtonReleasedEvent)}: {MouseButton}";
    }
}
