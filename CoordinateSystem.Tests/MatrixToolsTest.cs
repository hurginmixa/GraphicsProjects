using CoordinateSystem.Primitives;
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
            var v = new MatrixArray(new[,] {{3.0}, {4.0}, {1.0}});

            {
                MatrixArray m = MatrixArray.MakeRotateMatrixArray(PI / 2);
                MatrixArray r = MatrixArray.Mul(m, v);
                Assert.That(r.Matrix, Is.EqualTo(new[,] {{-4}, {3}, {1}}).Within(0.0001));
            }

            {
                MatrixArray m = MatrixArray.MakeRotateMatrixArray(-PI / 2);
                MatrixArray r = MatrixArray.Mul(m, v);
                Assert.That(r.Matrix, Is.EqualTo(new[,] {{4}, {-3}, {1}}).Within(0.0001));
            }

            {
                var m1 = MatrixArray.MakeRotateMatrixArray(-PI / 4);

                MatrixArray m = MatrixArray.MakeRotateMatrixArray(0);
                m = MatrixArray.Mul(m1, m);
                m = MatrixArray.Mul(m1, m);

                MatrixArray r = MatrixArray.Mul(m, v);
                Assert.That(r.Matrix, Is.EqualTo(new[,] {{4}, {-3}, {1}}).Within(0.0001));
            }
        });
    }
}