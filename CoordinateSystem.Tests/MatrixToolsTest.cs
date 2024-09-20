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
            var v = new[,] {{3.0}, {4.0}, {1.0}};

            {
                var m = MakeAngleMatrix(PI / 2);
                var r = MultiplyMatrices(m, v);
                Assert.That(r, Is.EqualTo(new[,] {{-4}, {3}, {1}}).Within(0.0001));
            }

            {
                var m = MakeAngleMatrix(-PI / 2);
                var r = MultiplyMatrices(m, v);
                Assert.That(r, Is.EqualTo(new[,] {{4}, {-3}, {1}}).Within(0.0001));
            }

            {
                var m1 = MakeAngleMatrix(-PI / 4);

                var m = MakeAngleMatrix(0);
                m = MultiplyMatrices(m1, m);
                m = MultiplyMatrices(m1, m);

                var r = MultiplyMatrices(m, v);
                Assert.That(r, Is.EqualTo(new[,] {{4}, {-3}, {1}}).Within(0.0001));
            }
        });
    }
}