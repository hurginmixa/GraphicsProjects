namespace CoordinateSystem.Tests;

[TestFixture]
internal class MatrixTest
{
    [Test]
    public void Test1()
    {
        Matrix m1 = Matrix.GetFromAngle(Math.PI / 4);
        Matrix m2 = Matrix.GetFromAngle(Math.PI / 4);

        Matrix m3 = Matrix.Mul(m1, m2);

        Vector v = Matrix.Mul(m3, new Vector(1, 1));
    }
}