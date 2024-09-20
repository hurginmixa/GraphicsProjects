namespace CoordinateSystem;

public class Matrix
{
    // AA AB DA
    // BA BB DB
    public readonly double AA, AB, DA;
    public readonly double BA, BB, DB;

    public Matrix(double[,] v) : this(aa: v[0, 0], ab: v[0, 1], ba: v[1, 0], bb: v[1, 1], da: v[0, 2], db: v[1, 2]) { }

    public Matrix(double aa, double ab, double ba, double bb, double da, double db)
    {
        AA = aa; AB = ab; DA = da;
        BA = ba; BB = bb; DB = db;
    }

    public static Matrix One => new Matrix(aa: 1, ab: 0, ba: 0, bb: 1, da: 0, db: 0);

    public double[,] ToArray() => new[,] {{AA, AB, DA}, {BA, BB, DB}, {0, 0, 1}};

    public static Matrix GetFromAngle(double algRad) => new(aa: Math.Cos(algRad), ab: -Math.Sin(algRad), ba: Math.Sin(algRad), bb: Math.Cos(algRad), da: 0, db: 0);

    public static Matrix Mul(Matrix m1, Matrix m2)
    {
        double[,] v = MatrixTools.MultiplyMatrices(m1.ToArray(), m2.ToArray());

        return new Matrix(v);
    }

    public static Vector Mul(Matrix m, Vector m2)
    {
        var v = MatrixTools.MultiplyMatrices(m.ToArray(), m2.ToArray());

        return new Vector(v);
    }

    public static Matrix Mul(Matrix m, double k)
    {
        double aa = m.AA * k;
        double ab = m.AB * k;
        double ba = m.BA * k;
        double bb = m.BB * k;
        return new Matrix(aa: aa, ab: ab, ba: ba, bb: bb, m.DA, m.DB);
    }
}