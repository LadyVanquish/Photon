namespace Photon;

public abstract class AppPlatform : IDisposable
{
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

    public abstract void Run();

    public abstract void RequestExit();

    protected void OnExit(int exitCode)
    {
        Application?.OnPlatformExit(exitCode);
    }

    public virtual void Dispose()
    {
        MainWindow?.Dispose();
        GC.SuppressFinalize(this);
    }
}
