using Photon.Events;
using Photon.Window;

namespace Photon;

public abstract class PhotonWindow : IDisposable
{
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

    public virtual void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
