using Photon.Events;
using Photon.Window;

namespace Photon;

public abstract class PhotonWindow : IDisposable
{
    private bool _disposed;
    private bool _active;

    public abstract string Title { get; set; }
    public Size ClientArea { get; protected set; }
    public abstract bool VSync { get; set; }
    public bool Active
    {
        get => _active;
        private set
        {
            if (_active == value)
            {
                return;
            }
            _active = value;
            if (_active)
            {
                OnActivated?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                OnDeactivated?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public event EventHandler? OnActivated;
    public event EventHandler? OnDeactivated;


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

    protected void ProcessEvent(PhotonEvent args)
    {
        if (args is WindowActivateEvent)
        {
            Active = true;
            return;
        }
        if (args is WindowDeactivateEvent)
        {
            Active = false;
            return;
        }
        // Don't process events while inactive
        if (!Active)
        {
            return;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~PhotonWindow()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
