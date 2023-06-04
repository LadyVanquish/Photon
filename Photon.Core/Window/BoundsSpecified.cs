namespace Photon.Window;

[Flags]
public enum BoundsSpecified
{
    None = 0,
    X = 1 << 0,
    Y = 1 << 1,
    Width = 1 << 2,
    Height = 1 << 3,
    Location = X | Y,
    Size = Width | Height,
    All = Location | Size
}
