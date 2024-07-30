namespace CoordinateSystem;

public struct Point<TCoordinateSystem>(double x, double y) where TCoordinateSystem : CoordinateSystem
{
    public static Shift<TCoordinateSystem> operator- (Point<TCoordinateSystem> p1, Point<TCoordinateSystem> p2) => new(p1.X - p2.X, p1.Y - p2.Y);

    public static Point<TCoordinateSystem> Origin => new Point<TCoordinateSystem>(0, 0);

    public override string ToString() => $"X:{X}, X:{Y}";

    public readonly double X = x, Y = y;
    public readonly bool IsInited = true;
}