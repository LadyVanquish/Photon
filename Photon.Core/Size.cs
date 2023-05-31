using System.Diagnostics.CodeAnalysis;

namespace Photon;

public struct Size : IEquatable<Size>
{
    public static readonly Size Empty;

    private int _width;
    private int _height;

    public int Width { readonly get => _width; set => _width = value; }
    public int Height { readonly get => _height; set => _height = value; }

    public Size(int width, int height)
    {
        _width = width;
        _height = height;
    }

    public readonly bool Equals(Size other)
    {
        return _width == other._width && _height == other._height;
    }

    public override readonly bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Size size && Equals(size);
    }

    public override readonly int GetHashCode()
    {
        return HashCode.Combine(_width, _height);
    }

    public override readonly string ToString()
    {
        return $"[{_width}, {_height}]";
    }

    public static bool operator ==(Size left, Size right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Size left, Size right)
    {
        return !left.Equals(right);
    }
}
