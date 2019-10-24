using NUnit.Framework;
using RayTracerChallenge;

namespace Tests
{
    [TestFixture]
    public class ColorTest
    {
        [Test]
        public void PropertiesWired()
        {
            var c = new FColor(-0.5, 0.4, 1.7);
            Assert.That(c.Red, Is.EqualTo(-0.5), "Red");
            Assert.That(c.Green, Is.EqualTo(0.4), "Green");
            Assert.That(c.Blue, Is.EqualTo(1.7), "Blue");
        }

        [Test]
        public void Add()
        {
            var c1 = new FColor(.9, .6, .75);
            var c2 = new FColor(.7, .1, .25);
            var sum = new FColor(1.6, 0.7, 1.0);
            Assert.That(c1 + c2, Is.EqualTo(sum));
        }

        [Test]
        public void Subtract()
        {
            var c1 = new FColor(.9, .6, .75);
            var c2 = new FColor(.7, .1, .25);
            var diff = new FColor(.2, 0.5, .5);
            Assert.That(c1 - c2, Is.EqualTo(diff));
        }

        [Test]
        public void MultScalar()
        {
            var c = new FColor(.2, .3, .4);
            var scalar = 2.0;
            var prod = new FColor(.4, .6, .8);
            Assert.That(c * scalar, Is.EqualTo(prod));
        }

        [Test]
        public void BlendColors()
        {
            var c1 = new FColor(1, .2, .4);
            var c2 = new FColor(.9, 1, .1);
            var blend = new FColor(.9, .2, .04);
            Assert.That(c1 * c2, Is.EqualTo(blend));
        }
    }
}
