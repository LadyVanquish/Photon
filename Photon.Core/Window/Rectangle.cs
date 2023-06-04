using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Photon.Window;

[StructLayout(LayoutKind.Sequential)]
public struct Rectangle : IEquatable<Rectangle>
{
    public static readonly Rectangle Empty;

    private int _x;
    private int _y;
    private int _width;
    private int _height;

    public int X { readonly get => _x; set => _x = value; }
    public int Y { readonly get => _y; set => _y = value; }
    public int Width { readonly get => _width; set => _width = value; }
    public int Height { readonly get => _height; set => _height = value; }
    [Browsable(false)]
    public Point Location
    {
        readonly get => new(_x, _y);
        set
        {
            _x = value.X;
            _y = value.Y;
        }
    }
    [Browsable(false)]
    public Size Size
    {
        readonly get => new(_width, _height);
        set
        {
            _width = value.Width;
            _height = value.Height;
        }
    }
    [Browsable(false)] public readonly int Left => _x;
    [Browsable(false)] public readonly int Top => _y;
    [Browsable(false)] public readonly int Right => _x + _width;
    [Browsable(false)] public readonly int Bottom => _y + _height;
    [Browsable(false)] public readonly bool IsEmpty => _x == 0 && _y == 0 && _width == 0 && _height == 0;

    public Rectangle(int x, int y, int width, int height)
    {
        _x = x;
        _y = y;
        _width = width;
        _height = height;
    }

    public Rectangle(Point location, Size size)
    {
        _x = location.X;
        _y = location.Y;
        _width = size.Width;
        _height = size.Height;
    }

    public static Rectangle FromLTRB(int left, int top, int right, int bottom)
    {
        return new Rectangle(left, top, right - left, bottom - top);
    }

    public static Rectangle Intersect(Rectangle x, Rectangle y)
    {
        int x1 = Math.Max(x._x, y._x);
        int x2 = Math.Min(x._x + x._width, y._x + y._width);
        int y1 = Math.Max(x._y, y._y);
        int y2 = Math.Min(x._y + x._height, y._y + y._height);

        if (x2 >= x1 && y2 >= y1)
        {
            return new Rectangle(x1, y1, x2 - x1, y2 - y1);
        }
        return Empty;
    }

    public static Rectangle Union(Rectangle x, Rectangle y)
    {
        int x1 = Math.Min(x._x, y._x);
        int x2 = Math.Max(x._x + x._width, y._x + y._width);
        int y1 = Math.Min(x._y, y._y);
        int y2 = Math.Max(x._y + x._height, y._y + y._height);

        return new Rectangle(x1, y1, x2 - x1, y2 - y1);
    }

    public readonly bool Contains(int x, int y)
    {
        return _x <= x && x < _x + _width && _y <= y && y < _y + _height;
    }

    public readonly bool Contains(Point point)
    {
        return Contains(point.X, point.Y);
    }

    public readonly bool Intersects(Rectangle rectangle)
    {
        return rectangle._x < _x + _width && _x < rectangle._x + rectangle._width && rectangle._y < _y + _height && _y < rectangle._y + rectangle._height;
    }

    public readonly bool Contains(Rectangle rectangle)
    {
        return _x <= rectangle._x && rectangle._x + rectangle._width <= _x + _width && _y <= rectangle._y && rectangle._y + rectangle._height <= _y + _height;
    }

    public void Inflate(int width, int height)
    {
        _x -= width;
        _y -= height;
        _width += 2 * width;
        _height += 2 * height;
    }

    public void Inflate(Size size)
    {
        Inflate(size.Width, size.Height);
    }

    public void Intersect(Rectangle rectangle)
    {
        Rectangle result = Intersect(rectangle, this);

        _x = result._x;
        _y = result._y;
        _width = result._width;
        _height = result._height;
    }

    public void Offset(int x, int y)
    {
        _x += x;
        _y += y;
    }

    public readonly bool Equals(Rectangle other)
    {
        return _x == other._x && _y == other._y && _width == other._width && _height == other._height;
    }

    public override readonly bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Rectangle objT && Equals(objT);
    }

    public override readonly int GetHashCode()
    {
        return HashCode.Combine(_x, _y, _width, _height);
    }

    public override readonly string ToString()
    {
        return $"[{_x}, {_y}, {_width}, {_height}]";
    }

    public static bool operator ==(Rectangle x, Rectangle y)
    {
        return x.Equals(y);
    }

    public static bool operator !=(Rectangle x, Rectangle y)
    {
        return !x.Equals(y);
    }
}
