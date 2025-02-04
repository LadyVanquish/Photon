using System.Diagnostics;

namespace Photon;

public struct Timer
{
    private long _startTime;
    private long _lastTime;
    private int _pauseCount;
    private long _pauseStartTime;
    private long _timePaused;

    public TimeSpan StartTime { readonly get; private set; }
    public TimeSpan TotalTime { readonly get; private set; }
    public TimeSpan TotalTimeWithPause { readonly get; private set; }
    public TimeSpan ElapsedTime { readonly get; private set; }
    public TimeSpan ElapsedTimeWithPause { readonly get; private set; }
    public double SpeedFactor { readonly get; set; }
    public readonly bool IsPaused => _pauseCount > 0;

    public Timer()
    {
        SpeedFactor = 1.0;
        Reset();
    }

    public Timer(TimeSpan startTime)
    {
        SpeedFactor = 1.0;
        Reset(startTime);
    }

    public void Reset()
    {
        Reset(TimeSpan.Zero);
    }

    public void Reset(TimeSpan startTime)
    {
        StartTime = startTime;
        TotalTime = startTime;
        _startTime = Stopwatch.GetTimestamp();
        _lastTime = _startTime;
        _pauseCount = 0;
        _pauseStartTime = 0;
        _timePaused = 0;
    }

    public void Tick()
    {
        if (IsPaused)
        {
            _timePaused = Stopwatch.GetTimestamp() - _pauseStartTime;
            ElapsedTime = TimeSpan.Zero;
            ElapsedTimeWithPause = Utilities.ConvertRawToTimestamp(_timePaused);

            return;
        }

        long time = Stopwatch.GetTimestamp();
        TotalTime = StartTime + new TimeSpan((long)Math.Round(Utilities.ConvertRawToTimestamp(time - _timePaused - _startTime).Ticks * SpeedFactor));
        TotalTimeWithPause = StartTime + new TimeSpan((long)Math.Round(Utilities.ConvertRawToTimestamp(time - _startTime).Ticks * SpeedFactor));

        ElapsedTime = Utilities.ConvertRawToTimestamp(time - _timePaused - _lastTime);
        ElapsedTimeWithPause = Utilities.ConvertRawToTimestamp(time - _lastTime);

        if (ElapsedTime < TimeSpan.Zero)
        {
            ElapsedTime = TimeSpan.Zero;
        }

        _lastTime = time;
    }

    public void Pause()
    {
        if (++_pauseCount == 1)
        {
            _pauseStartTime = Stopwatch.GetTimestamp();
            _timePaused = 0;
        }
    }

    public void Resume()
    {
        if (--_pauseCount <= 0)
        {
            _timePaused = Stopwatch.GetTimestamp() - _pauseStartTime;
        }
    }
}
