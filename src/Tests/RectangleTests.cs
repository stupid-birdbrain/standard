using NUnit.Framework;
using Standard;
using System.Numerics;

namespace Tests;

[TestFixture]
public class RectangleTests {
    [Test]
    public void RectanglesIntersect() {
        var rect1 = new Rectangle(0, 0, 10, 10);
        var rect2 = new Rectangle(5, 5, 10, 10);
        
        bool isIntersecting = rect1.Intersects(rect2);
        
        Assert.That(isIntersecting, Is.True);
    }
    
    [Test]
    public void RectanglesDontIntersect() {
        var rect1 = new Rectangle(0, 0, 10, 10);
        var rect2 = new Rectangle(11, 11, 10, 10);
        
        bool isIntersecting = rect1.Intersects(rect2);
        
        Assert.That(isIntersecting, Is.False);
    }
    
    [Test]
    public void RectangleMove() {
        var rect1 = new Rectangle(0, 0, 10, 10);
        
        Assert.That(rect1.Position, Is.EqualTo(new Vector2(0, 0)));

        rect1 = rect1.Moved(new Vector2(0, 10));
        
        Assert.That(rect1.Position, Is.EqualTo(new Vector2(0, 10)));
    }
    
    [Test]
    public void RectangleVLine() {
        var rect1 = new Rectangle(5, 5, 3, 3);

        var line = new Line(new Vector2(0, 0), new Vector2(10, 10));
        
        var intersects = line.Intersects(rect1);
        
        Assert.That(intersects, Is.True);
    }
}