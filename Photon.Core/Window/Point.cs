using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Photon.Window;

[StructLayout(LayoutKind.Sequential)]
public struct Point : IEquatable<Point>
{
    public static readonly Point Empty;

    private int _x;
    private int _y;

    public int X { readonly get => _x; set => _x = value; }
    public int Y { readonly get => _y; set => _y = value; }
    [Browsable(false)] public readonly bool IsEmpty => _x == 0 && _y == 0;

    public Point(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public Point(Size size)
    {
        _x = size.Width;
        _y = size.Height;
    }

    public Point(int dw)
    {
        unchecked
        {
            _x = (short)LOWORD(dw);
            _y = (short)HIWORD(dw);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int HIWORD(int n)
    {
        return (n >> 16) & 0xFFFF;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int LOWORD(int n)
    {
        return n & 0xFFFF;
    }

    public static Point Add(Point x, Size y)
    {
        return new Point(x._x + y.Width, x._y + y.Height);
    }

    public static Point Subtract(Point x, Size y)
    {
        return new Point(x._x - y.Width, x._y - y.Height);
    }

    public void Offset(int dx, int dy)
    {
        _x += dx;
        _y += dy;
    }

    public void Offset(Point x)
    {
        _x += x._x;
        _y += x._y;
    }

    public readonly bool Equals(Point other)
    {
        return other._x == _x && other._y == _y;
    }

    public override readonly bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Point objT && Equals(objT);
    }

    public override readonly int GetHashCode()
    {
        return HashCode.Combine(_x, _y);
    }

    public override readonly string ToString()
    {
        return $"[{_x}, {_y}]";
    }

    public static bool operator ==(Point x, Point y)
    {
        return x.Equals(y);
    }

    public static bool operator !=(Point x, Point y)
    {
        return !x.Equals(y);
    }

    public static Point operator +(Point x, Size y)
    {
        return Add(x, y);
    }

    public static Point operator -(Point x, Size y)
    {
        return Subtract(x, y);
    }

    public static explicit operator Size(Point x)
    {
        return new Size(x._x, x._y);
    }
}
