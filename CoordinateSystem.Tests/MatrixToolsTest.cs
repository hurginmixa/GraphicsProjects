using NUnit.Framework.Internal;

namespace CoordinateSystem.Tests;

using static Math;
using static MatrixTools;

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
                MatrixArray m = MakeRotateMatrix(PI / 2);
                MatrixArray r = MultiplyMatrices(m, v);
                Assert.That(r.Matrix, Is.EqualTo(new[,] {{-4}, {3}, {1}}).Within(0.0001));
            }

            {
                MatrixArray m = MakeRotateMatrix(-PI / 2);
                MatrixArray r = MultiplyMatrices(m, v);
                Assert.That(r.Matrix, Is.EqualTo(new[,] {{4}, {-3}, {1}}).Within(0.0001));
            }

            {
                var m1 = MakeRotateMatrix(-PI / 4);

                MatrixArray m = MakeRotateMatrix(0);
                m = MultiplyMatrices(m1, m);
                m = MultiplyMatrices(m1, m);

                MatrixArray r = MultiplyMatrices(m, v);
                Assert.That(r.Matrix, Is.EqualTo(new[,] {{4}, {-3}, {1}}).Within(0.0001));
            }
        });
    }
}