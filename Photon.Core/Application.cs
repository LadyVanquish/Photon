using System.Diagnostics;

namespace Photon;

public abstract class Application : IDisposable
{
    public static Application? Current { get; private set; }

    private readonly AppPlatform _platform;

    private bool _disposed;

    public PhotonWindow? MainWindow => _platform.MainWindow;
    public bool EnableVerticalSync { get; set; } = true;
    public float AspectRatio => MainWindow is null ? 0.0f : (float)MainWindow.ClientArea.Width / MainWindow.ClientArea.Height;

    public event EventHandler<ExitEventArgs>? Exit;

    public Application(AppPlatform platform)
    {
        Debug.Assert(Current is null);

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

    protected abstract void Render();

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

    public void Tick()
    {
        if (!BeginDraw())
        {
            return;
        }
        Render();
        EndDraw();
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
