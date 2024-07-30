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
}