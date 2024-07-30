namespace CoordinateSystem;

public class Vector(double x, double y)
{
    public Vector(double[,] m) : this(m[0,0], m[1,0]) { }

    public readonly double X = x, Y = y;

    public double[,] ToArray() => new[,] {{X}, {Y}, {1}};
}