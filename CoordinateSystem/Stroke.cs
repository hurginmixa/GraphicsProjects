namespace CoordinateSystem;

public readonly struct Stroke<TCoordinateSystem> where TCoordinateSystem : CoordinateSystem
{
    private readonly Point<TCoordinateSystem> _point1;
    private readonly Point<TCoordinateSystem> _point2;

    public Stroke(Point<TCoordinateSystem> point1, Point<TCoordinateSystem> point2)
    {
        _point1 = point1;
        _point2 = point2;
    }

    public Stroke(Point<TCoordinateSystem> point, Shift<TCoordinateSystem> size)
    {
        _point1 = point;
        _point2 = _point1 + size;
    }

    public Point<TCoordinateSystem> Point1 => _point1;

    public Point<TCoordinateSystem> Point2 => _point2;

    public Shift<TCoordinateSystem> Size => _point2 - _point1;

    public double Length
    {
        get
        {
            Shift<TCoordinateSystem> size = Size;
            return double.Sqrt(size.DX * size.DX + size.DY * size.DY);
        }
    }
}