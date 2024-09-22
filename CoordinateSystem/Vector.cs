namespace CoordinateSystem;

public class Vector(double x, double y)
{
    public Vector(MatrixArray m) : this(m.Matrix[0,0], m.Matrix[1,0]) { }

    public readonly double X = x, Y = y;

    public MatrixArray ToMatrixArray() => new MatrixArray(new[,] {{X}, {Y}, {1}});
}