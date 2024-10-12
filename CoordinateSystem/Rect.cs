using CoordinateSystem.Privitives;

namespace CoordinateSystem;

public readonly struct Rect<TCoordinateSystem> : IShape<TCoordinateSystem> where TCoordinateSystem : CoordinateSystem
{
    private readonly Stroke<TCoordinateSystem> _mainDiagonal;
    private readonly Stroke<TCoordinateSystem> _subDiagonal;

    public Rect(Point<TCoordinateSystem> point1, Point<TCoordinateSystem> point2) : this(point1, point2 - point1)
    {
    }

    public Rect(Point<TCoordinateSystem> point, Shift<TCoordinateSystem> size)
    {
        _mainDiagonal = new Stroke<TCoordinateSystem>(point, size);

        double newY1 = _mainDiagonal.Point1.Y + size.DY;
        double newY2 = _mainDiagonal.Point2.Y - size.DY;

        _subDiagonal = new Stroke<TCoordinateSystem>(
            new Point<TCoordinateSystem>(_mainDiagonal.Point1.X, newY1),
            new Point<TCoordinateSystem>(_mainDiagonal.Point2.X, newY2));
    }

    public Rect(Stroke<TCoordinateSystem> mainDiagonal, Stroke<TCoordinateSystem> subDiagonal)
    {
        _mainDiagonal = mainDiagonal;
        _subDiagonal = subDiagonal;
    }

    public Stroke<TCoordinateSystem> MainDiagonal => _mainDiagonal;

    public Stroke<TCoordinateSystem> SubDiagonal => _subDiagonal;

    Point<TCoordinateSystem> IShape<TCoordinateSystem>.MinPoint
    {
        get
        {
            Point<TCoordinateSystem> mainDiagonalMinPoint = _mainDiagonal.Point1;
            Point<TCoordinateSystem> subDiagonalMinPoint = _subDiagonal.Point1;

            return new Point<TCoordinateSystem>(
                Math.Min(mainDiagonalMinPoint.X, subDiagonalMinPoint.X), 
                Math.Min(mainDiagonalMinPoint.Y, subDiagonalMinPoint.Y));
        }
    }

    Point<TCoordinateSystem> IShape<TCoordinateSystem>.MaxPoint
    {
        get
        {
            Point<TCoordinateSystem> mainDiagonalMaxPoint = _mainDiagonal.Point2;
            Point<TCoordinateSystem> subDiagonalMaxPoint = _subDiagonal.Point2;

            return new Point<TCoordinateSystem>(
                Math.Max(mainDiagonalMaxPoint.X, subDiagonalMaxPoint.X), 
                Math.Max(mainDiagonalMaxPoint.Y, subDiagonalMaxPoint.Y));
        }
    }

    public IEnumerable<Stroke<TCoordinateSystem>> GetStrokes()
    {
        Point<TCoordinateSystem> mainMinPoint = _mainDiagonal.Point1;
        Point<TCoordinateSystem> mainMaxPoint = _mainDiagonal.Point2;

        Point<TCoordinateSystem> subMinPoint = _subDiagonal.Point1;
        Point<TCoordinateSystem> subMaxPoint = _subDiagonal.Point2;

        Point<TCoordinateSystem> minX_minY = new Point<TCoordinateSystem>(mainMinPoint.X, mainMinPoint.Y);
        Point<TCoordinateSystem> minX_MaxY = new Point<TCoordinateSystem>(subMinPoint.X, subMinPoint.Y);
        Point<TCoordinateSystem> maxX_maxY = new Point<TCoordinateSystem>(mainMaxPoint.X, mainMaxPoint.Y);
        Point<TCoordinateSystem> maxX_minY = new Point<TCoordinateSystem>(subMaxPoint.X, subMaxPoint.Y);

        yield return new Stroke<TCoordinateSystem>(minX_minY, minX_MaxY);
        yield return new Stroke<TCoordinateSystem>(minX_MaxY, maxX_maxY);
        yield return new Stroke<TCoordinateSystem>(maxX_maxY, maxX_minY);
        yield return new Stroke<TCoordinateSystem>(maxX_minY, minX_minY);
    }

    public IShape<TTargetSystem> Transform<TTargetSystem>(Transform<TCoordinateSystem, TTargetSystem> transform)
        where TTargetSystem : CoordinateSystem => transform * this;
}