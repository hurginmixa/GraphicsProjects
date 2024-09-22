namespace CoordinateSystem;

// AA AB DA
// BA BB DB
public class Matrix(double aa, double ab, double ba, double bb, double da, double db)
{
    private readonly double _aa = aa;
    private readonly double _ab = ab;
    private readonly double _ba = ba;
    private readonly double _bb = bb;
    private readonly double _da = da;
    private readonly double _db = db;

    public Matrix(MatrixArray v) : this(aa: v.Matrix[0, 0], ab: v.Matrix[0, 1], ba: v.Matrix[1, 0], bb: v.Matrix[1, 1], da: v.Matrix[0, 2], db: v.Matrix[1, 2]) { }

    public MatrixArray ToMatrixArray() => new(new[,] {{_aa, _ab, _da}, {_ba, _bb, _db}, {0, 0, 1}});

    public static Matrix GetFromAngle(double algRad) => new(MatrixTools.MakeRotateMatrix(algRad));

    public static Matrix Mul(Matrix m1, Matrix m2)
    {
        MatrixArray result = MatrixTools.MultiplyMatrices(m1.ToMatrixArray(), m2.ToMatrixArray());
        
        return new Matrix(result);
    }

    public static Vector Mul(Matrix m, Vector m2)
    {
        MatrixArray v = MatrixTools.MultiplyMatrices(m.ToMatrixArray(), m2.ToMatrixArray());

        return new Vector(v);
    }
}