using System.Numerics;
using System.Runtime.CompilerServices;
using CoordinateSystem.Privitives;

namespace CoordinateSystem;

public class Transform<TSrcCoordSystem, TDestCoordSystem>
    where TSrcCoordSystem : CoordinateSystem
    where TDestCoordSystem : CoordinateSystem
{
    private Matrix _matrix = Matrix.MakeRotateMatrixArray(0);

    public void AddRotate(double angle)
    {
        _matrix = Matrix.Mul(Matrix.MakeRotateMatrixArray(angle), _matrix);
    }

    public void AddFlipX()
    {
        Matrix m = new Matrix(aa: -1, ab: 0, ba: 0, bb: 1, da: 0, db: 0);

        _matrix = Matrix.Mul(m, _matrix);
    }

    public void AddFlipY()
    {
        Matrix m = new Matrix(aa: 1, ab: 0, ba: 0, bb: -1, da: 0, db: 0);

        _matrix = Matrix.Mul(m, _matrix);
    }

    public void AddShift(Shift<TDestCoordSystem> shift)
    {
        var g = new Matrix(1, 0, 0, 1, shift.DX, shift.DY);

        _matrix = Matrix.Mul(g, _matrix);
    }

    public void AddStretch(double d)
    {
        var r = _matrix;

        var s = new Matrix(aa: d, ab: 0, ba: 0, bb: d, da: 0, db: 0);

        _matrix = Matrix.Mul(_matrix, s);
    }

    public static Point<TDestCoordSystem> operator* (Transform<TSrcCoordSystem, TDestCoordSystem> tr, Point<TSrcCoordSystem> point)
    {
        Matrix m = tr._matrix;

        Matrix pointMatrix = new Matrix(new[,] {{point.X}, {point.Y}, {1}});

        Matrix resultMatrix = Matrix.Mul(m, pointMatrix);

        return new Point<TDestCoordSystem>(resultMatrix.MatrixCoefficients[0, 0], resultMatrix.MatrixCoefficients[1, 0]);
    }

    public static Stroke<TDestCoordSystem> operator* (Transform<TSrcCoordSystem, TDestCoordSystem> tr, Stroke<TSrcCoordSystem> stroke)
    {
        Point<TDestCoordSystem> p1 = tr * stroke.Point1;
        Point<TDestCoordSystem> p2 = tr * stroke.Point2;

        return new Stroke<TDestCoordSystem>(p1, p2);
    }

    public static Rect<TDestCoordSystem> operator* (Transform<TSrcCoordSystem, TDestCoordSystem> tr, Rect<TSrcCoordSystem> rect)
    {
        Stroke<TDestCoordSystem> p1 = tr * rect.MainDiagonal;
        Stroke<TDestCoordSystem> p2 = tr * rect.SubDiagonal;

        return new Rect<TDestCoordSystem>(p1, p2);
    }

    public static Shift<TDestCoordSystem> operator* (Transform<TSrcCoordSystem, TDestCoordSystem> tr, Shift<TSrcCoordSystem> shift)
    {
        var p1 = Point<TSrcCoordSystem>.Origin + shift;

        var p2 = tr * p1;

        return p2 - Point<TDestCoordSystem>.Origin;
    }
}