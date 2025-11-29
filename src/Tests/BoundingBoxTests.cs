using NUnit.Framework;
using Standard;
using System.Numerics;

namespace Tests;

[TestFixture]
public sealed class BoundingBoxTests {
    [Test]
    public void Intersects_BoxContainedWithinAnother_ReturnsTrue() {
        var outerBox = new BoundingBox(new Vector3(-5, -5, -5), new Vector3(5, 5, 5));
        var innerBox = new BoundingBox(new Vector3(-1, -1, -1), new Vector3(1, 1, 1));

        Assert.That(outerBox.Intersects(innerBox), Is.True);
        Assert.That(innerBox.Intersects(outerBox), Is.True);
    }
    
        [Test]
    public void GetCorners_ReturnsEightCorners() {
        var box = new BoundingBox(new Vector3(0, 0, 0), new Vector3(1, 1, 1));
        var corners = box.GetCorners();

        Assert.That(corners.Length, Is.EqualTo(8));
    }

    [Test]
    public void GetCorners_ReturnsCorrectCornerValues() {
        var min = new Vector3(0, 0, 0);
        var max = new Vector3(1, 1, 1);
        var box = new BoundingBox(min, max);
        var actualCorners = box.GetCorners();

        var expectedCorners = new[] {
            new Vector3(0, 0, 0),
            new Vector3(1, 0, 0), 
            new Vector3(1, 1, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, 0, 1),
            new Vector3(1, 0, 1),
            new Vector3(1, 1, 1),
            new Vector3(0, 1, 1)
        };

        foreach (var expectedCorner in expectedCorners) {
            Assert.That(actualCorners.ToArray(), Does.Contain(expectedCorner), $"expected corner {expectedCorner} not found!");
        }

        Assert.That(actualCorners[0], Is.EqualTo(new Vector3(min.X, min.Y, min.Z)));
        Assert.That(actualCorners[1], Is.EqualTo(new Vector3(max.X, min.Y, min.Z)));
        Assert.That(actualCorners[2], Is.EqualTo(new Vector3(max.X, max.Y, min.Z)));
        Assert.That(actualCorners[3], Is.EqualTo(new Vector3(min.X, max.Y, min.Z)));
        Assert.That(actualCorners[4], Is.EqualTo(new Vector3(min.X, min.Y, max.Z)));
        Assert.That(actualCorners[5], Is.EqualTo(new Vector3(max.X, min.Y, max.Z)));
        Assert.That(actualCorners[6], Is.EqualTo(new Vector3(max.X, max.Y, max.Z)));
        Assert.That(actualCorners[7], Is.EqualTo(new Vector3(min.X, max.Y, max.Z)));
    }
}