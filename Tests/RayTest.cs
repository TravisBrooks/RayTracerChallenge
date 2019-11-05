using NUnit.Framework;
using RayTracerChallenge;

namespace Tests
{
    [TestFixture]
    public class RayTest
    {
        [Test]
        public void CreateAndQuery()
        {
            var origin = Tuple3D.Point(1, 2, 3);
            var direction = Tuple3D.Vector(4, 5, 6);
            var ray = new Ray(origin, direction);

            Assert.That(ray.OriginPoint, Is.EqualTo(origin), "origin");
            Assert.That(ray.DirectionVector, Is.EqualTo(direction), "direction");
        }

        [Test]
        public void PointFromDistance()
        {
            var ray = new Ray(Tuple3D.Point(2, 3, 4), Tuple3D.Vector(1, 0, 0));
            var pos0 = ray.Position(0);
            var pos1 = ray.Position(1);
            var posNeg1 = ray.Position(-1);
            var pos2pt5 = ray.Position(2.5);

            Assert.That(pos0, Is.EqualTo(Tuple3D.Point(2, 3, 4)), "pos0");
            Assert.That(pos1, Is.EqualTo(Tuple3D.Point(3, 3, 4)), "pos1");
            Assert.That(posNeg1, Is.EqualTo(Tuple3D.Point(1, 3, 4)), "posNeg1");
            Assert.That(pos2pt5, Is.EqualTo(Tuple3D.Point(4.5, 3, 4)), "pos2pt5");
        }
    }
}
