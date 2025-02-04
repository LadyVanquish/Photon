using System.Diagnostics;

namespace Photon;

public abstract class Application : IDisposable
{
    public static Application? Current { get; private set; }

    protected string _title;

    private readonly AppPlatform _platform;
    private Timer _timer = new();
    private readonly TimeSpan _maximumElapsedTime = TimeSpan.FromMilliseconds(500);
    private TimeSpan _accumulatedElapsedTime;
    private GameTime _updateTime;
    private GameTime _drawTime;

    private bool _disposed;

    public PhotonWindow? MainWindow => _platform.MainWindow;
    public bool EnableVerticalSync { get; set; } = true;
    public float AspectRatio => MainWindow is null ? 0.0f : (float)MainWindow.ClientArea.Width / MainWindow.ClientArea.Height;
    public TimeSpan TargetElapsedTime { get; set; }
    public bool IsFixedTimestep { get; set; }
    public bool ForceUpdatePerDraw { get; set; }
    public bool DrawDesynchronized { get; set; }
    public float DrawInterpolationFactor { get; private set; }

    public event EventHandler<ExitEventArgs>? Exit;

    public Application(string title, AppPlatform platform)
    {
        Debug.Assert(Current is null);

        _title = title;
        _platform = platform;
        _platform.Ready += HandlePlatformReady;

        Current = this;
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

    internal void HandlePlatformReady(object? sender, EventArgs args)
    {
        Initialize();
    }

    internal void OnPlatformExit(int exitCode)
    {
        Exit?.Invoke(this, new ExitEventArgs(exitCode));
    }

    internal void OnPlatformKeyboardEvent(KeyboardKey key, bool pressed)
    {
        OnKeyboardEvent(key, pressed);
    }

    internal void OnDisplayChange()
    {
    }

    protected abstract void Update(ref GameTime gameTime);

    protected abstract void Draw(ref GameTime gameTime);

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _platform?.Dispose();
                Current = null;
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            _disposed = true;
        }
    }

    private TimeSpan _timeSinceLastFpsRedraw = TimeSpan.Zero;

    public void Tick()
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

    public void Run()
    {
        _platform.Run();
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    ~Application()
    {
        Dispose(disposing: false);
    }
}
