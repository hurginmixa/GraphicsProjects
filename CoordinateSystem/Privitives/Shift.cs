using CommonTools;

namespace CoordinateSystem.Privitives;

public readonly struct Shift<TCoordinateSystem>(double dx, double dy) : IEquatable<Shift<TCoordinateSystem>> where TCoordinateSystem : CoordinateSystem
{
    public readonly double DX = dx, DY = dy;
    public readonly bool IsInitialized = true;

    public static Point<TCoordinateSystem> operator +(Point<TCoordinateSystem> point, Shift<TCoordinateSystem> shift) => new(point.X + shift.DX, point.Y + shift.DY);

    public static Point<TCoordinateSystem> operator -(Point<TCoordinateSystem> point, Shift<TCoordinateSystem> shift) => new(point.X - shift.DX, point.Y - shift.DY);

    public bool Equals(Shift<TCoordinateSystem> other)
    {
        return DX.IsEquals(other.DX) && DY.IsEquals(other.DY) && IsInitialized == other.IsInitialized;
    }

    public override bool Equals(object? obj)
    {
        return obj is Shift<TCoordinateSystem> other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(DX, DY, IsInitialized);
    }
}