using System;
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
            Assert.That(xs[0].Time, Is.EqualTo(4), "xs[0]");
            Assert.That(xs[1].Time, Is.EqualTo(6), "xs[1]");
        }

        [Test]
        public void RayIntersectsSphereAtTangent()
        {
            var ray = new Ray(Tuple3D.Point(0, 1, -5), Tuple3D.Vector(0, 0, 1));
            var sphere = new Sphere();

            var xs = sphere.Intersect(ray);
            Assert.That(xs.Count, Is.EqualTo(2), "intersection point count");
            Assert.That(xs[0].Time, Is.EqualTo(5), "xs[0]");
            Assert.That(xs[1].Time, Is.EqualTo(5), "xs[1]");
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
            Assert.That(xs[0].Time, Is.EqualTo(-1), "xs[0]");
            Assert.That(xs[1].Time, Is.EqualTo(1), "xs[1]");
        }

        [Test]
        public void SphereBehindRay()
        {
            var ray = new Ray(Tuple3D.Point(0, 0, 5), Tuple3D.Vector(0, 0, 1));
            var sphere = new Sphere();

            var xs = sphere.Intersect(ray);
            Assert.That(xs.Count, Is.EqualTo(2), "intersection point count");
            Assert.That(xs[0].Time, Is.EqualTo(-6), "xs[0]");
            Assert.That(xs[1].Time, Is.EqualTo(-4), "xs[1]");
        }

        [Test]
        public void IntersectSetsObjectOnIntersection()
        {
            var r = new Ray(Tuple3D.Point(0, 0, -5), Tuple3D.Vector(0, 0, 1));
            var s = new Sphere();
            var xs = s.Intersect(r);
            Assert.That(xs.Count, Is.EqualTo(2), "list count");
            Assert.That(xs[0].TheObject, Is.EqualTo(s), "intersection 1");
            Assert.That(xs[1].TheObject, Is.EqualTo(s), "intersection 2");
        }

        [Test]
        public void SphereDefaultTransformation()
        {
            var s = new Sphere();
            Assert.That(s.Transform, Is.EqualTo(Matrix.Identity()));
        }

        [Test]
        public void ChangingSphereTransformation()
        {
            var s = new Sphere();
            var t = Transformation.Translation(2, 3, 4);
            s.Transform = t;
            Assert.That(s.Transform, Is.EqualTo(t));
        }

        [Test]
        public void IntersectScaledSphereWithRay()
        {
            var ray = new Ray(Tuple3D.Point(0, 0, -5), Tuple3D.Vector(0, 0, 1));
            var sphere = new Sphere
            {
                Transform = Transformation.Scaling(2, 2, 2)
            };
            var xs = sphere.Intersect(ray);
            Assert.That(xs.Count, Is.EqualTo(2), "intersection point count");
            Assert.That(xs[0].Time, Is.EqualTo(3), "xs[0]");
            Assert.That(xs[1].Time, Is.EqualTo(7), "xs[1]");
        }

        [Test]
        public void IntersectTranslatedSphereWithRay()
        {
            var ray = new Ray(Tuple3D.Point(0, 0, -5), Tuple3D.Vector(0, 0, 1));
            var sphere = new Sphere();
            sphere.Transform = Transformation.Translation(5, 0, 0);
            var xs = sphere.Intersect(ray);
            Assert.That(xs.Count, Is.EqualTo(0), "intersection point count");
        }

        [Test]
        public void NormalPointOnX()
        {
            var sphere = new Sphere();
            var pt = Tuple3D.Point(1, 0, 0);
            var expected = Tuple3D.Vector(1, 0, 0);
            var actual = sphere.NormalAt(pt);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void NormalPointOnY()
        {
            var sphere = new Sphere();
            var pt = Tuple3D.Point(0, 1, 0);
            var expected = Tuple3D.Vector(0, 1, 0);
            var actual = sphere.NormalAt(pt);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void NormalPointOnZ()
        {
            var sphere = new Sphere();
            var pt = Tuple3D.Point(0, 0, 1);
            var expected = Tuple3D.Vector(0, 0, 1);
            var actual = sphere.NormalAt(pt);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void NormalPointOnNonaxialPoint()
        {
            var sphere = new Sphere();
            var weirdVal = Math.Sqrt(3) / 3.0;
            var pt = Tuple3D.Point(weirdVal, weirdVal, weirdVal);
            var expected = Tuple3D.Vector(weirdVal, weirdVal, weirdVal);
            var actual = sphere.NormalAt(pt);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void NormalIsNormalizedVector()
        {
            var sphere = new Sphere();
            var weirdVal = Math.Sqrt(3) / 3.0;
            var pt = Tuple3D.Point(weirdVal, weirdVal, weirdVal);
            var actual = sphere.NormalAt(pt);
            var actualNormalized = actual.Normalize();
            Assert.That(actual, Is.EqualTo(actualNormalized));
        }

        [Test]
        public void NormalOnTranslatedSphere()
        {
            var sphere = new Sphere
            {
                Transform = Transformation.Translation(0, 1, 0)
            };
            var pt = Tuple3D.Point(0, 1.70711, -0.70711);
            var expected = Tuple3D.Vector(0, 0.70711, -0.70711);
            var actual = sphere.NormalAt(pt);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void NormalOnTransformedSphere()
        {
            var sphere = new Sphere
            {
                //Transform = Transformation.Scaling(1, 0.5, 1) * Transformation.RotationZ(Math.PI/5.0)
                Transform = Transformation.Scaling(1, 0.5, 1)
                                          .RotateZ(Math.PI/5.0)
            };
            var pt = Tuple3D.Point(0, Math.Sqrt(2)/2.0, -Math.Sqrt(2) / 2.0);
            var expected = Tuple3D.Vector(0, 0.97014, -0.24254);
            var actual = sphere.NormalAt(pt);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
