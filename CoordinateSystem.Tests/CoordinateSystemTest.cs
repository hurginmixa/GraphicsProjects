namespace CoordinateSystem.Tests;

[TestFixture]
internal class CoordinateSystemTest
{
    [Test]
    public void Test1()
    {
        Point<DisplaySystem> point = new Point<DisplaySystem>(12, 4) + new Shift<DisplaySystem>(5, 7);

        Assert.That(point, Is.EqualTo(new Point<DisplaySystem>(17, 11)));
    }

    [Test]
    public void Test2()
    {
        Shift<DisplaySystem> shift = new Point<DisplaySystem>(12, 4) - new Point<DisplaySystem>(5, 7);

        Assert.That(shift, Is.EqualTo(new Shift<DisplaySystem>(7, -3)));
    }

    [Test]
    public void Test3()
    {
        Point<DisplaySystem> point = new Point<DisplaySystem>(12, 4) - new Shift<DisplaySystem>(5, 7);

        Assert.That(point, Is.EqualTo(new Point<DisplaySystem>(7, -3)));
    }
}