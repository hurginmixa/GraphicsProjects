using CommonTools;

namespace CoordinateSystem;

public readonly struct Point<TCoordinateSystem>(double x, double y) : IEquatable<Point<TCoordinateSystem>> where TCoordinateSystem : CoordinateSystem
{
    public readonly double X = x, Y = y;
    public readonly bool IsInitialized = true;

    public static Shift<TCoordinateSystem> operator- (Point<TCoordinateSystem> p1, Point<TCoordinateSystem> p2) => new(p1.X - p2.X, p1.Y - p2.Y);

    public static Point<TCoordinateSystem> Origin => new(0, 0);

    public override string ToString() => $"X:{X}, Y:{Y}";

    public bool Equals(Point<TCoordinateSystem> other)
    {
        return X.IsEquals(other.X) && Y.IsEquals(other.Y) && IsInitialized == other.IsInitialized;
    }

    public override bool Equals(object? obj)
    {
        return obj is Point<TCoordinateSystem> other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, IsInitialized);
    }
}