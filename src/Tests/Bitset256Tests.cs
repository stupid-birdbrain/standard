using NUnit.Framework;
using NUnit.Framework.Legacy;
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
    
    [Test]
    public void Bitset256_Enumerate() {
        Bitset256 bs = default;
        bs.Set(5);
        bs.Set(0);
        bs.Set(64);
        bs.Set(255);

        List<int> expectedBits = new List<int> { 0, 5, 64, 255 };
        List<int> actualBits = new List<int>();

        foreach (int bitIndex in bs) {
            actualBits.Add(bitIndex);
        }

        Assert.That(actualBits, Is.EquivalentTo(expectedBits));
        Assert.That(actualBits, Is.Ordered);
        Assert.That(actualBits.Count, Is.EqualTo(bs.PopCount()));
    }
}