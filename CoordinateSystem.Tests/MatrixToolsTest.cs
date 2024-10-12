using CoordinateSystem.Privitives;
using NUnit.Framework.Internal;

namespace CoordinateSystem.Tests;

using static Math;

[TestFixture]
public class MatrixToolsTest
{
    [SetUp]
    public void Setup()
    {
        Test test = TestExecutionContext.CurrentContext.CurrentTest;
    }

    [TearDown]
    public void TearDown()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.Multiple(() =>
        {
            var v = new Matrix(new[,] {{3.0}, {4.0}, {1.0}});

            {
                Matrix m = Matrix.MakeRotateMatrixArray(PI / 2);
                Matrix r = Matrix.Mul(m, v);
                Assert.That(r.MatrixCoefficients, Is.EqualTo(new[,] {{-4}, {3}, {1}}).Within(0.0001));
            }

            {
                Matrix m = Matrix.MakeRotateMatrixArray(-PI / 2);
                Matrix r = Matrix.Mul(m, v);
                Assert.That(r.MatrixCoefficients, Is.EqualTo(new[,] {{4}, {-3}, {1}}).Within(0.0001));
            }

            {
                var m1 = Matrix.MakeRotateMatrixArray(-PI / 4);

                Matrix m = Matrix.MakeRotateMatrixArray(0);
                m = Matrix.Mul(m1, m);
                m = Matrix.Mul(m1, m);

                Matrix r = Matrix.Mul(m, v);
                Assert.That(r.MatrixCoefficients, Is.EqualTo(new[,] {{4}, {-3}, {1}}).Within(0.0001));
            }
        });
    }
}