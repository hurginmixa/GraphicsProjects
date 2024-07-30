namespace CoordinateSystem;

public readonly struct Shift<TCoordinateSystem>(double dx, double dy) where TCoordinateSystem : CoordinateSystem
{
    public static Point<TCoordinateSystem> operator+ (Point<TCoordinateSystem> point, Shift<TCoordinateSystem> shift) => new(point.X + shift.DX, point.Y + shift.DY);

    public readonly double DX = dx, DY = dy;
    public readonly bool IsInited = true;

    public Shift<DisplayCoordSystem> FlipX() => new(-DX, DY);
    public Shift<DisplayCoordSystem> FlipY() => new(DX, -DY);
}