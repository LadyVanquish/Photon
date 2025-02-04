namespace Photon;

public struct GameTime
{
    private TimeSpan _accumulatedElapsedTime;
    private int _accumulatedFramesPerSecond;
    private double _factor;

    public TimeSpan Elapsed { get; private set; }
    public TimeSpan Total { get; private set; }
    public int FrameCount { get; private set; }
    public float FramesPerSecond {  get; private set; }
    public TimeSpan FrameTime { get; private set; }
    public bool FramesPerSecondUpdated {  get; private set; }
    public TimeSpan WarpElapsed { get; private set; }
    public double Factor
    {
        readonly get => _factor;
        set => _factor = value > 0 ? value : 0;
    }

    public GameTime()
    {
        _accumulatedElapsedTime = TimeSpan.Zero;
        _factor = 1;
    }

    public GameTime(TimeSpan totalTime, TimeSpan elapsedTime)
    {
        Total = totalTime;
        Elapsed = elapsedTime;
        _accumulatedElapsedTime = TimeSpan.Zero;
        _factor = 1;
    }

    internal void Update(TimeSpan totalGameTime, TimeSpan elapsedGameTime, bool incrementFrameCount)
    {
        Total = totalGameTime;
        Elapsed = elapsedGameTime;
        WarpElapsed = TimeSpan.FromTicks((long)(Elapsed.Ticks * Factor));

        FramesPerSecondUpdated = false;

        if (incrementFrameCount)
        {
            _accumulatedElapsedTime += elapsedGameTime;
            double accumulatedElapsedSeconds = _accumulatedElapsedTime.TotalSeconds;
            if (_accumulatedFramesPerSecond > 0 && accumulatedElapsedSeconds > 1)
            {
                FrameTime = TimeSpan.FromTicks(_accumulatedElapsedTime.Ticks / _accumulatedFramesPerSecond);
                FramesPerSecond = (float)(_accumulatedFramesPerSecond / accumulatedElapsedSeconds);
                _accumulatedFramesPerSecond = 0;
                _accumulatedElapsedTime = TimeSpan.Zero;
                FramesPerSecondUpdated = true;
            }
            ++_accumulatedFramesPerSecond;
            ++FrameCount;
        }
    }

    internal void Reset(TimeSpan totalGameTime)
    {
        Update(totalGameTime, TimeSpan.Zero, false);
        _accumulatedElapsedTime = TimeSpan.Zero;
        _accumulatedFramesPerSecond = 0;
        FrameCount = 0;
    }
}
