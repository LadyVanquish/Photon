using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Photon.Window;

[StructLayout(LayoutKind.Sequential)]
public struct Size : IEquatable<Size>
{
    public static readonly Size Empty;

    private int _width;
    private int _height;

    public int Width { readonly get => _width; set => _width = value; }
    public int Height { readonly get => _height; set => _height = value; }
    [Browsable(false)] public readonly bool IsEmpty => _width == 0 && _height == 0;

    public Size(int width, int height)
    {
        _width = width;
        _height = height;
    }

    public Size(Point point)
    {
        _width = point.X;
        _height = point.Y;
    }

    public static Size Add(Size x, Size y)
    {
        return new Size(x._width + y._width, x._height + y._height);
    }

    public static Size Subtract(Size x, Size y)
    {
        return new Size(x._width - y._width, x._height - y._height);
    }

    public readonly bool Equals(Size other)
    {
        return _width == other._width && _height == other._height;
    }

    public override readonly bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Size objT && Equals(objT);
    }

    public override readonly int GetHashCode()
    {
        return HashCode.Combine(_width, _height);
    }

    public override readonly string ToString()
    {
        return $"[{_width}, {_height}]";
    }

    public static bool operator ==(Size x, Size y)
    {
        return x.Equals(y);
    }

    public static bool operator !=(Size x, Size y)
    {
        return !x.Equals(y);
    }

    public static Size operator +(Size x, Size y)
    {
        return Add(x, y);
    }

    public static Size operator -(Size x, Size y)
    {
        return Subtract(x, y);
    }
}
