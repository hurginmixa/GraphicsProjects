using NUnit.Framework.Internal;
using static System.Math;

namespace CoordinateSystem.Tests;

[TestFixture]
internal class TransformTest
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
    public void RotateTest()
    {
        Transform<GraphicCoordSystem, DisplayCoordSystem> transform = new Transform<GraphicCoordSystem, DisplayCoordSystem>();

        Point<GraphicCoordSystem> point = new Point<GraphicCoordSystem>(3, 4);

        Point<DisplayCoordSystem> r = transform * point;
        Assert.That(r, Is.EqualTo(new Point<DisplayCoordSystem>(3, 4)));

        transform.AddRotate(PI / 2);
        r = transform * point;
        Assert.That(r, Is.EqualTo(new Point<DisplayCoordSystem>(-4, 3)));

        transform.AddRotate(-PI / 2);
        r = transform * point;
        Assert.That(r, Is.EqualTo(new Point<DisplayCoordSystem>(3, 4)));

        transform.AddRotate(-PI / 2);
        r = transform * point;
        Assert.That(r, Is.EqualTo(new Point<DisplayCoordSystem>(4, -3)));
    }

    [Test]
    public void FlipsTest()
    {
        Transform<GraphicCoordSystem, DisplayCoordSystem> transform = new Transform<GraphicCoordSystem, DisplayCoordSystem>();

        Point<GraphicCoordSystem> point = new Point<GraphicCoordSystem>(3, 4);

        transform.AddFlipX();
        Point<DisplayCoordSystem> r = transform * point;
        Assert.That(r, Is.EqualTo(new Point<DisplayCoordSystem>(-3, 4)));

        transform.AddFlipX();
        r = transform * point;
        Assert.That(r, Is.EqualTo(new Point<DisplayCoordSystem>(3, 4)));

        transform.AddFlipY();
        r = transform * point;
        Assert.That(r, Is.EqualTo(new Point<DisplayCoordSystem>(3, -4)));

        transform.AddFlipY();
        r = transform * point;
        Assert.That(r, Is.EqualTo(new Point<DisplayCoordSystem>(3, 4)));

        transform.AddFlipY();
        transform.AddFlipX();
        r = transform * point;
        Assert.That(r, Is.EqualTo(new Point<DisplayCoordSystem>(-3, -4)));

        transform.AddRotate(PI);
        r = transform * point;
        Assert.That(r, Is.EqualTo(new Point<DisplayCoordSystem>(3, 4)));
    }
}