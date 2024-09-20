namespace CoordinateSystem.Tests;

[TestFixture]
internal class CoordinateSystemTest
{
    [Test]
    public void Test1()
    {
        Point<DisplayCoordSystem> point = new Point<DisplayCoordSystem>(12, 4) + new Shift<DisplayCoordSystem>(5, 7);

        Assert.That(point, Is.EqualTo(new Point<DisplayCoordSystem>(17, 11)));
    }

    [Test]
    public void Test2()
    {
        Shift<DisplayCoordSystem> shift = new Point<DisplayCoordSystem>(12, 4) - new Point<DisplayCoordSystem>(5, 7);

        Assert.That(shift, Is.EqualTo(new Shift<DisplayCoordSystem>(7, -3)));
    }

    [Test]
    public void Test3()
    {
        Point<DisplayCoordSystem> point = new Point<DisplayCoordSystem>(12, 4) - new Shift<DisplayCoordSystem>(5, 7);

        Assert.That(point, Is.EqualTo(new Point<DisplayCoordSystem>(7, -3)));
    }
}