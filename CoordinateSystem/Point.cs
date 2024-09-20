using MixaSystem;

namespace CoordinateSystem;

public struct Point<TCoordinateSystem>(double x, double y) : IEquatable<Point<TCoordinateSystem>> where TCoordinateSystem : CoordinateSystem
{
    public readonly double X = x, Y = y;
    public readonly bool IsInitialized = true;

    public static Shift<TCoordinateSystem> operator- (Point<TCoordinateSystem> p1, Point<TCoordinateSystem> p2) => new(p1.X - p2.X, p1.Y - p2.Y);

    public static Point<TCoordinateSystem> Origin => new Point<TCoordinateSystem>(0, 0);

    public override string ToString() => $"X:{X}, X:{Y}";

    public bool Equals(Point<TCoordinateSystem> other)
    {
        return X.IsEquals1(other.X, 1e-6) && Y.IsEquals1(other.Y, 1e-6) && IsInitialized == other.IsInitialized;
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