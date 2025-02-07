using System.Diagnostics;

namespace Photon;

public abstract class Application : IDisposable
{
    public static Application? Current { get; private set; }

    public abstract PhotonWindow? MainWindow { get; }
    public Dictionary<IntPtr, PhotonWindow> Windows { get; } = [];
    public bool EnableVerticalSync { get; set; } = true;
    public TimeSpan TargetElapsedTime { get; set; }
    public bool IsFixedTimestep { get; set; }
    public bool ForceUpdatePerDraw { get; set; }
    public bool DrawDesynchronized { get; set; }
    public float DrawInterpolationFactor { get; private set; }

    public event EventHandler? Ready;
    public event EventHandler<ExitEventArgs>? Exit;

    public Application(string title)
    {
        Debug.Assert(Current is null);

        _title = title;
        Initialize();

        Current = this;
    }

    public abstract void Run();

    public virtual void Dispose()
    {
        Current = null;
        GC.SuppressFinalize(this);
    }

    protected virtual void Initialize()
    {
    }

    protected virtual bool BeginDraw()
    {
        return true;
    }

    protected virtual void EndDraw()
    {
    }

    protected virtual void OnKeyboardEvent(KeyboardKey key, bool pressed)
    {
    }

    protected void OnReady()
    {
        Ready?.Invoke(this, EventArgs.Empty);
    }

    protected void OnExit(int exitCode)
    {
        Exit?.Invoke(this, new ExitEventArgs(exitCode));
    }

    protected abstract void Update(ref GameTime gameTime);

    protected abstract void Draw(ref GameTime gameTime);

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                Current = null;
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            _disposed = true;
        }
    }

    protected void Tick()
    {
        _timer.Tick();
        TimeSpan elapsedAdjustedTime = _timer.ElapsedTimeWithPause;
        if (elapsedAdjustedTime > _maximumElapsedTime)
        {
            elapsedAdjustedTime = _maximumElapsedTime;
        }

        bool drawFrame = true;
        int updateCount = 1;
        TimeSpan singleFrameElapsedTime = elapsedAdjustedTime;
        long drawLag = 0;

        if (IsFixedTimestep)
        {
            if (Math.Abs(elapsedAdjustedTime.Ticks - TargetElapsedTime.Ticks) < (TargetElapsedTime.Ticks >> 6))
            {
                elapsedAdjustedTime = TargetElapsedTime;
            }

            _accumulatedElapsedTime += elapsedAdjustedTime;

            if (!ForceUpdatePerDraw)
            {
                updateCount = (int)(_accumulatedElapsedTime.Ticks / TargetElapsedTime.Ticks);
            }

            if (DrawDesynchronized)
            {
                drawLag = _accumulatedElapsedTime.Ticks % TargetElapsedTime.Ticks;
            }
            else if (updateCount == 0)
            {
                return;
            }

            _accumulatedElapsedTime = new TimeSpan(_accumulatedElapsedTime.Ticks - (updateCount * TargetElapsedTime.Ticks));
            singleFrameElapsedTime = TargetElapsedTime;
        }

        TimeSpan totalElapsedTime = TimeSpan.Zero;

        if (!BeginDraw())
        {
            return;
        }

        for (int idx = 0; idx < updateCount; ++idx)
        {
            _updateTime.Update(_updateTime.Total + singleFrameElapsedTime, singleFrameElapsedTime, true);
            Update(ref _updateTime);
            totalElapsedTime += singleFrameElapsedTime;
        }

        if (drawFrame)
        {
            DrawInterpolationFactor = drawLag / (float)TargetElapsedTime.Ticks;
            _drawTime.Factor = _updateTime.Factor;
            _drawTime.Update(_drawTime.Total + totalElapsedTime, totalElapsedTime, true);

            Draw(ref _drawTime);
        }
        EndDraw();

        _timeSinceLastFpsRedraw += _timer.ElapsedTimeWithPause;
        if (_timeSinceLastFpsRedraw > TimeSpan.FromSeconds(1))
        {
            _timeSinceLastFpsRedraw = TimeSpan.Zero;
            MainWindow!.Title = $"{_title} - FPS: {_drawTime.FramesPerSecond} {_drawTime.FrameTime.TotalMilliseconds}ms - Logic FPS: {_updateTime.FramesPerSecond} {_updateTime.FrameTime.TotalMilliseconds}ms";
        }
    }

    private readonly string _title;
    private bool _disposed;
    private Timer _timer = new();
    private readonly TimeSpan _maximumElapsedTime = TimeSpan.FromMilliseconds(500);
    private TimeSpan _accumulatedElapsedTime;
    private GameTime _updateTime;
    private GameTime _drawTime;
    private TimeSpan _timeSinceLastFpsRedraw = TimeSpan.Zero;
}
