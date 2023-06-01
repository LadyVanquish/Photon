namespace Photon.Events;

public abstract class KeyEvent : PhotonEvent
{
    public KeyboardKey KeyCode { get; }

    public KeyEvent(KeyboardKey keyCode) : base(EventCategory.Keyboard | EventCategory.Input)
    {
        KeyCode = keyCode;
    }
}

public sealed class KeyPressedEvent : KeyEvent
{
    public int RepeatCount { get; }

    public KeyPressedEvent(int repeatCount, KeyboardKey keyCode) : base(keyCode)
    {
        RepeatCount = repeatCount;
    }

    public override string ToString()
    {
        return $"{nameof(KeyPressedEvent)}: {KeyCode} ({RepeatCount} times)";
    }
}

public sealed class KeyReleasedEvent : KeyEvent
{
    public KeyReleasedEvent(KeyboardKey keyCode) : base(keyCode)
    {
    }

    public override string ToString()
    {
        return $"{nameof(KeyReleasedEvent)}: {KeyCode}";
    }
}
