namespace Photon;

public sealed class ExitEventArgs(int exitCode) : EventArgs
{
    public int ExitCode { get; } = exitCode;
}
