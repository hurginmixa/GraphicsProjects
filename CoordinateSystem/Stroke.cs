using CoordinateSystem.Privitives;

namespace CoordinateSystem;

public readonly struct Stroke<TCoordinateSystem> : IShape<TCoordinateSystem> where TCoordinateSystem : CoordinateSystem
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

    Point<TCoordinateSystem> IShape<TCoordinateSystem>.MinPoint => new(Math.Min(_point1.X, _point2.X), Math.Min(_point1.Y, _point2.Y));

    Point<TCoordinateSystem> IShape<TCoordinateSystem>.MaxPoint => new(Math.Max(_point1.X, _point2.X), Math.Max(_point1.Y, _point2.Y));

    public IEnumerable<Stroke<TCoordinateSystem>> GetStrokes() => new[] {this};

    public IShape<TTargetSystem> Transform<TTargetSystem>(Transform<TCoordinateSystem, TTargetSystem> transform)
        where TTargetSystem : CoordinateSystem => transform * this;

    public double Length
    {
        get
        {
            Shift<TCoordinateSystem> size = Size;
            return double.Sqrt(size.DX * size.DX + size.DY * size.DY);
        }
    }
}