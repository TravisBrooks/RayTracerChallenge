using NUnit.Framework;
using RayTracerChallenge;

namespace Tests
{
    [TestFixture]
    public class IntersectionTest
    {
        [Test]
        public void IntersectionEncapsulatesTimeAndObject()
        {
            var sphere = new Sphere();
            var time = 3.5;
            var intersection = new Intersection(time, sphere);
            Assert.That(intersection.Time, Is.EqualTo(time), "time");
            Assert.That(sphere, Is.EqualTo(intersection.TheObject), "object");
        }

        [Test]
        public void AggregatingIntersections()
        {
            var sphere = new Sphere();
            var i1 = new Intersection(1, sphere);
            var i2 = new Intersection(2, sphere);
            var list = new IntersectionList(i1, i2);
            Assert.That(list.Count, Is.EqualTo(2), "list count");
            Assert.That(list[0].Time, Is.EqualTo(1), "t1");
            Assert.That(list[1].Time, Is.EqualTo(2), "t2");
        }

        [Test]
        public void HitAllIntersectionsPositiveT()
        {
            var sphere = new Sphere();
            var i1 = new Intersection(1, sphere);
            var i2 = new Intersection(2, sphere);
            var list = new IntersectionList(i1, i2);
            var i = list.Hit();
            Assert.That(i, Is.EqualTo(i1));
        }

        [Test]
        public void HitSomeIntersectionsNegativeT()
        {
            var sphere = new Sphere();
            var i1 = new Intersection(-1, sphere);
            var i2 = new Intersection(1, sphere);
            var list = new IntersectionList(i1, i2);
            var i = list.Hit();
            Assert.That(i, Is.EqualTo(i2));
        }

        [Test]
        public void HitAllIntersectionsNegativeT()
        {
            var sphere = new Sphere();
            var i1 = new Intersection(-2, sphere);
            var i2 = new Intersection(-1, sphere);
            var list = new IntersectionList(i1, i2);
            var i = list.Hit();
            Assert.That(i, Is.Null);
        }

        [Test]
        public void HitAlwaysLowestNonNegativeIntersection()
        {
            var sphere = new Sphere();
            var i1 = new Intersection(5, sphere);
            var i2 = new Intersection(7, sphere);
            var i3 = new Intersection(-3, sphere);
            var i4 = new Intersection(2, sphere);
            var list = new IntersectionList(i1, i2, i3, i4);
            var i = list.Hit();
            Assert.That(i, Is.EqualTo(i4));
        }
    }
}
