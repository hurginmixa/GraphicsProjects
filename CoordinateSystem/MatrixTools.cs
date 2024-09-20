using static System.Math;

namespace CoordinateSystem
{
    public static class MatrixTools
    {
        public static double[,] MultiplyMatrices(double[,] a, double[,] b)
        {
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

            return result;
        }

        public static double[,] MakeAngleMatrix(double algRad) => new[,] {{Cos(algRad), -Sin(algRad), 0.0}, {Sin(algRad), Cos(algRad), 0.0}, {0.0, 0.0, 1.0}};
    }
}
