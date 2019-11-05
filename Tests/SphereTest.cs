using NUnit.Framework;
using RayTracerChallenge;

namespace Tests
{
    [TestFixture]
    public class SphereTest
    {
        [Test]
        public void RayIntersectsSphereTwoPoint()
        {
            var ray = new Ray(Tuple3D.Point(0, 0, -5), Tuple3D.Vector(0, 0, 1));
            var sphere = new Sphere();

            var xs = sphere.Intersect(ray);
            Assert.That(xs.Count, Is.EqualTo(2), "intersection point count");
            Assert.That(xs[0], Is.EqualTo(4), "xs[0]");
            Assert.That(xs[1], Is.EqualTo(6), "xs[1]");
        }

        [Test]
        public void RayIntersectsSphereAtTangent()
        {
            var ray = new Ray(Tuple3D.Point(0, 1, -5), Tuple3D.Vector(0, 0, 1));
            var sphere = new Sphere();

            var xs = sphere.Intersect(ray);
            Assert.That(xs.Count, Is.EqualTo(2), "intersection point count");
            Assert.That(xs[0], Is.EqualTo(5), "xs[0]");
            Assert.That(xs[1], Is.EqualTo(5), "xs[1]");
        }

        [Test]
        public void RayMissesSphere()
        {
            var ray = new Ray(Tuple3D.Point(0, 2, -5), Tuple3D.Vector(0, 0, 1));
            var sphere = new Sphere();

            var xs = sphere.Intersect(ray);
            Assert.That(xs.Count, Is.EqualTo(0), "intersection point count");
        }

        [Test]
        public void RayOriginatesInsideSphere()
        {
            var ray = new Ray(Tuple3D.Point(0, 0, 0), Tuple3D.Vector(0, 0, 1));
            var sphere = new Sphere();

            var xs = sphere.Intersect(ray);
            Assert.That(xs.Count, Is.EqualTo(2), "intersection point count");
            Assert.That(xs[0], Is.EqualTo(-1), "xs[0]");
            Assert.That(xs[1], Is.EqualTo(1), "xs[1]");
        }

        [Test]
        public void SphereBehindRay()
        {
            var ray = new Ray(Tuple3D.Point(0, 0, 5), Tuple3D.Vector(0, 0, 1));
            var sphere = new Sphere();

            var xs = sphere.Intersect(ray);
            Assert.That(xs.Count, Is.EqualTo(2), "intersection point count");
            Assert.That(xs[0], Is.EqualTo(-6), "xs[0]");
            Assert.That(xs[1], Is.EqualTo(-4), "xs[1]");
        }
    }
}
