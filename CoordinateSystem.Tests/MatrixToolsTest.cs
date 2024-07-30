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
        double[,] GetMatrix(double algRad) => new[,] {{Cos(algRad), -Sin(algRad), 0.0}, {Sin(algRad), Cos(algRad), 0.0}, {0.0, 0.0, 1.0}};

        Assert.Multiple(() =>
        {
            var m = GetMatrix(PI / 2);
            var v = new[,] {{3.0}, {4.0}, {1.0}};
            var r = MatrixTools.MultiplyMatrices(m, v);
            Assert.That(r, Is.EqualTo(new[,] {{-4}, {3}, {1}}).Within(0.0001));

            m = GetMatrix(-PI / 2);
            r = MatrixTools.MultiplyMatrices(m, v);
            Assert.That(r, Is.EqualTo(new[,] {{4}, {-3}, {1}}).Within(0.0001));
        });
    }
}