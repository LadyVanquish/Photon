﻿using System.Diagnostics;

namespace Photon;

public abstract class Application : IDisposable
{
    public static Application? Current { get; private set; }

    private readonly AppPlatform _platform;

    private bool _disposed;

    public Window? MainWindow => _platform.MainWindow;
    public bool EnableVerticalSync { get; set; } = true;
    public float AspectRatio => MainWindow is null ? 0.0f : (float)MainWindow.ClientSize.Width / MainWindow.ClientSize.Height;

    public event EventHandler<EventArgs>? Disposed;

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
        if (key == KeyboardKey.Escape && pressed)
        {
            Exit();
        }
    }

    internal void HandlePlatformReady(object? sender, EventArgs args)
    {
        Initialize();
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
                // TODO: dispose managed state (managed objects)
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

        if (_platform.IsBlockingRun)
        {
        }
    }

    public void Exit()
    {
        _platform.RequestExit();
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