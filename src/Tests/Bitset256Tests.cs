using NUnit.Framework;
using Standard;

namespace Tests;

[TestFixture]
public class Bitset256Tests {
    [Test]
    public void Default_IsZero() {
        Bitset256 bs = default;
        Assert.That(bs.IsZero, Is.True);
        Assert.That(bs.PopCount(), Is.EqualTo(0));
    }
}