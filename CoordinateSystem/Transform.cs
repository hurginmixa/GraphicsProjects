using System.Numerics;
using System.Runtime.CompilerServices;

namespace CoordinateSystem;

using static MatrixTools;

public class Transform<TSrcCoordSystem, TDestCoordSystem>
    where TSrcCoordSystem : CoordinateSystem
    where TDestCoordSystem : CoordinateSystem
{
    private Matrix _matrix = new(MakeRotateMatrix(0));

    public void AddRotate(double angle)
    {
        _matrix = new Matrix(MultiplyMatrices(MakeRotateMatrix(angle), _matrix.ToMatrixArray()));
    }

    public void AddFlipX()
    {
        Matrix m = new Matrix(aa: -1, ab: 0, ba: 0, bb: 1, da: 0, db: 0);

        _matrix = new Matrix(MultiplyMatrices(m.ToMatrixArray(), _matrix.ToMatrixArray()));
    }

    public void AddFlipY()
    {
        Matrix m = new Matrix(aa: 1, ab: 0, ba: 0, bb: -1, da: 0, db: 0);

        _matrix = new Matrix(MultiplyMatrices(m.ToMatrixArray(), _matrix.ToMatrixArray()));
    }

    public void AddShift(Shift<TDestCoordSystem> shift)
    {
        var g = new Matrix(1, 0, 0, 1, shift.DX, shift.DY);

        _matrix = new Matrix(MultiplyMatrices(g.ToMatrixArray(), _matrix.ToMatrixArray()));
    }

    public void AddStretch(double d)
    {
        var r = _matrix.ToMatrixArray();

        var s = new Matrix(aa: d, ab: 0, ba: 0, bb: d, da: 0, db: 0);

        var resultMatrix = MultiplyMatrices(_matrix.ToMatrixArray(), s.ToMatrixArray());

        _matrix = new Matrix(resultMatrix);
    }

    public static Point<TDestCoordSystem> operator* (Transform<TSrcCoordSystem, TDestCoordSystem> tr, Point<TSrcCoordSystem> p2)
    {
        Matrix m = tr._matrix;

        MatrixArray vector = new MatrixArray(new[,] {{p2.X}, {p2.Y}, {1}});

        var resultMatrux = MultiplyMatrices(m.ToMatrixArray(), vector);

        return new Point<TDestCoordSystem>(resultMatrux.Matrix[0, 0], resultMatrux.Matrix[1, 0]);
    }

    public static Stroke<TDestCoordSystem> operator *(Transform<TSrcCoordSystem, TDestCoordSystem> tr, Stroke<TSrcCoordSystem> stroke)
    {
        Point<TDestCoordSystem> p1 = tr * stroke.Point1;
        Point<TDestCoordSystem> p2 = tr * stroke.Point2;

        return new Stroke<TDestCoordSystem>(p1, p2);
    }

    public static Shift<TDestCoordSystem> operator* (Transform<TSrcCoordSystem, TDestCoordSystem> tr, Shift<TSrcCoordSystem> shift)
    {
        var p1 = Point<TSrcCoordSystem>.Origin + shift;

        var p2 = tr * p1;

        return p2 - Point<TDestCoordSystem>.Origin;
    }
}