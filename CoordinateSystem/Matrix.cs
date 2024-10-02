namespace CoordinateSystem;

public class Matrix
{
    public readonly double[,] MatrixCoefficients;

    public Matrix(double[,] matrixCoefficients)
    {
        MatrixCoefficients = matrixCoefficients;
    }

    // AA AB DA
    // BA BB DB
    public Matrix(double aa, double ab, double ba, double bb, double da, double db)
    {
        MatrixCoefficients = new[,] { { aa, ab, da }, { ba, bb, db }, { 0, 0, 1 } };
    }

    public static Matrix Mul(Matrix ma, Matrix mb)
    {
        double[,] a = ma.MatrixCoefficients;
        double[,] b = mb.MatrixCoefficients;

        int aRows = a.GetLength(0);
        int aCols = a.GetLength(1);
        int bRows = b.GetLength(0);
        int bCols = b.GetLength(1);

        if (aCols != bRows)
        {
            throw new InvalidOperationException("Number of columns in the first matrix must be equal to the number of rows in the second matrix.");
        }

        double[,] result = new double[aRows, bCols];

        for (int row = 0; row < aRows; row++)
        {
            for (int col = 0; col < bCols; col++)
            {
                for (int k = 0; k < aCols; k++)
                {
                    result[row, col] += a[row, k] * b[k, col];
                }
            }
        }

        return new Matrix(result);
    }

    public static Matrix MakeRotateMatrixArray(double algRad) => new Matrix(new[,] { { Math.Cos(algRad), -Math.Sin(algRad), 0.0 }, { Math.Sin(algRad), Math.Cos(algRad), 0.0 }, { 0.0, 0.0, 1.0 } });
}