namespace CoordinateSystem;

using static MatrixTools;

public class Transform<TSrcCoordSystem, TDestCoordSystem>
    where TSrcCoordSystem : CoordinateSystem
    where TDestCoordSystem : CoordinateSystem
{
    private Matrix _matrix;

    public Transform()
    {
        _matrix = new Matrix(MakeAngleMatrix(0));
    }

    public void AddRotate(double angle)
    {
        _matrix = new Matrix(MultiplyMatrices(MakeAngleMatrix(angle), _matrix.ToArray()));
    }

    public void AddFlipX()
    {
        Matrix m = new Matrix(aa: -1, ab: 0, ba: 0, bb: 1, da: 0, db: 0);

        _matrix = new Matrix(MultiplyMatrices(m.ToArray(), _matrix.ToArray()));
    }

    public void AddFlipY()
    {
        Matrix m = new Matrix(aa: 1, ab: 0, ba: 0, bb: -1, da: 0, db: 0);

        _matrix = new Matrix(MultiplyMatrices(m.ToArray(), _matrix.ToArray()));
    }

    public static Point<TDestCoordSystem> operator* (Transform<TSrcCoordSystem, TDestCoordSystem> tr, Point<TSrcCoordSystem> p2)
    {
        Matrix m = tr._matrix;

        double[,] vector = new double[,] {{p2.X}, {p2.Y}, {1}};

        double[,] multiplyMatrices = MultiplyMatrices(m.ToArray(), vector);

        return new Point<TDestCoordSystem>(multiplyMatrices[0, 0], multiplyMatrices[1, 0]);
    }

    public static Shift<TDestCoordSystem> operator* (Transform<TSrcCoordSystem, TDestCoordSystem> tr, Shift<TSrcCoordSystem> p2) => new(0, 0);

    public static Point<TSrcCoordSystem> operator% (Transform<TSrcCoordSystem, TDestCoordSystem> tr, Point<TDestCoordSystem> p2) => new(0, 0);
    
    public static Shift<TSrcCoordSystem> operator% (Transform<TSrcCoordSystem, TDestCoordSystem> tr, Shift<TDestCoordSystem> p2) => new(0, 0);
}