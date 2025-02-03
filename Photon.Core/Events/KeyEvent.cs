namespace Photon.Events;

public abstract class KeyEvent(int scanCode, KeyboardKey keyCode) : PhotonEvent(EventCategory.Keyboard | EventCategory.Input)
{
    public int ScanCode { get; } = scanCode;
    public KeyboardKey KeyCode { get; } = keyCode;
}

public sealed class KeyPressedEvent(int repeatCount, int scanCode, KeyboardKey keyCode) : KeyEvent(scanCode, keyCode)
{
    public int RepeatCount { get; } = repeatCount;

    public override string ToString()
    {
        return $"{nameof(KeyPressedEvent)}: {KeyCode} ([0x{ScanCode:x2}|{ScanCode:000}] {RepeatCount} times)";
    }
}

public sealed class KeyReleasedEvent(int scanCode, KeyboardKey keyCode) : KeyEvent(scanCode, keyCode)
{
    public override string ToString()
    {
        return $"{nameof(KeyReleasedEvent)}: {KeyCode} ([0x{ScanCode:x2}|{ScanCode:000}])";
    }
}
