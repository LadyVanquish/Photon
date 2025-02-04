using System.Diagnostics;

namespace Photon;

public static class Utilities
{
    public static TimeSpan ConvertRawToTimestamp(long delta)
    {
        return delta == 0 ? default : TimeSpan.FromTicks(delta * TimeSpan.TicksPerSecond / Stopwatch.Frequency);
    }
}
