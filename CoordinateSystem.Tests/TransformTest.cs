using CoordinateSystem.Privitives;
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
        Transform<GraphicSystem, DisplaySystem> transform = new Transform<GraphicSystem, DisplaySystem>();

        Point<GraphicSystem> point = new (3, 4);
        Point<GraphicSystem> point1 = new ();

        Point<DisplaySystem> r = transform * point;
        Assert.That(r, Is.EqualTo(new Point<DisplaySystem>(3, 4)));

        transform.AddRotate(PI / 2);
        r = transform * point;
        Assert.That(r, Is.EqualTo(new Point<DisplaySystem>(-4, 3)));

        transform.AddRotate(-PI / 2);
        r = transform * point;
        Assert.That(r, Is.EqualTo(new Point<DisplaySystem>(3, 4)));

        transform.AddRotate(-PI / 2);
        r = transform * point;
        Assert.That(r, Is.EqualTo(new Point<DisplaySystem>(4, -3)));
    }

    [Test]
    public void FlipsTest()
    {
        Transform<GraphicSystem, DisplaySystem> transform = new Transform<GraphicSystem, DisplaySystem>();

        Point<GraphicSystem> point = new Point<GraphicSystem>(3, 4);

        transform.AddFlipX();
        Point<DisplaySystem> r = transform * point;
        Assert.That(r, Is.EqualTo(new Point<DisplaySystem>(-3, 4)));

        transform.AddFlipX();
        r = transform * point;
        Assert.That(r, Is.EqualTo(new Point<DisplaySystem>(3, 4)));

        transform.AddFlipY();
        r = transform * point;
        Assert.That(r, Is.EqualTo(new Point<DisplaySystem>(3, -4)));

        transform.AddFlipY();
        r = transform * point;
        Assert.That(r, Is.EqualTo(new Point<DisplaySystem>(3, 4)));

        transform.AddFlipY();
        transform.AddFlipX();
        r = transform * point;
        Assert.That(r, Is.EqualTo(new Point<DisplaySystem>(-3, -4)));

        transform.AddRotate(PI);
        r = transform * point;
        Assert.That(r, Is.EqualTo(new Point<DisplaySystem>(3, 4)));
    }
}