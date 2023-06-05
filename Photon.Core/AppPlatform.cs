namespace Photon;

public abstract class AppPlatform : IDisposable
{
    public static Type? PlatformType { get; set; }
    private bool _disposed;

    public Application? Application { get; internal set; }
    public abstract bool IsBlockingRun { get; }
    public abstract PhotonWindow? MainWindow { get; }

    public event EventHandler<EventArgs>? Ready;

    protected AppPlatform()
    {
    }

    protected void OnReady()
    {
        Ready?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            _disposed = true;
        }
    }

    public abstract void Run();

    public abstract void RequestExit();

    protected void OnExit(int exitCode)
    {
        Application?.OnPlatformExit(exitCode);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    ~AppPlatform()
    {
        Dispose(disposing: false);
    }
}
