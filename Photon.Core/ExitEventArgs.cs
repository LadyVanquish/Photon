namespace Photon;

public sealed class ExitEventArgs : EventArgs
{
    public int ExitCode { get; }

    public ExitEventArgs(int exitCode)
    {
        ExitCode = exitCode;
    }
}
