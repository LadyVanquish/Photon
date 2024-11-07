namespace Photon.Events;

public abstract class KeyEvent(KeyboardKey keyCode) : PhotonEvent(EventCategory.Keyboard | EventCategory.Input)
{
    public KeyboardKey KeyCode { get; } = keyCode;
}

public sealed class KeyPressedEvent(int repeatCount, KeyboardKey keyCode) : KeyEvent(keyCode)
{
    public int RepeatCount { get; } = repeatCount;

    public override string ToString()
    {
        return $"{nameof(KeyPressedEvent)}: {KeyCode} ({RepeatCount} times)";
    }
}

public sealed class KeyReleasedEvent(KeyboardKey keyCode) : KeyEvent(keyCode)
{
    public override string ToString()
    {
        return $"{nameof(KeyReleasedEvent)}: {KeyCode}";
    }
}
