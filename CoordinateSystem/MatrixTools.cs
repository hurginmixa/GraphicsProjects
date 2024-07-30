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
                throw new InvalidOperationException(
                    "Number of columns in the first matrix must be equal to the number of rows in the second matrix.");
            }

            double[,] result = new double[aRows, bCols];

            for (int i = 0; i < aRows; i++)
            {
                for (int j = 0; j < bCols; j++)
                {
                    for (int k = 0; k < aCols; k++)
                    {
                        result[i, j] += a[i, k] * b[k, j];
                    }
                }
            }

            return result;
        }
    }
}
