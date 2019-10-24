using System;
using NUnit.Framework;
using RayTracerChallenge;
using Tuple = RayTracerChallenge.Tuple;

namespace Tests
{
    [TestFixture]
    public class TupleTest
    {
        [Test]
        public void VectorProperties()
        {
            var a = new Tuple(4.3, -4.2, 3.1, 1.0);
            Assert.That(a.X, Is.EqualTo(4.3), "X property");
            Assert.That(a.Y, Is.EqualTo(-4.2), "Y property");
            Assert.That(a.Z, Is.EqualTo(3.1), "Z property");
            Assert.That(a.W, Is.EqualTo(1.0), "W property");
            Assert.That(a.IsPoint, Is.True, "is a point");
            Assert.That(a.IsVector, Is.False, "is not a vector");
        }

        [Test]
        public void PointProperties()
        {
            var a = new Tuple(4.3, -4.2, 3.1, 0.0);
            Assert.That(a.X, Is.EqualTo(4.3), "X property");
            Assert.That(a.Y, Is.EqualTo(-4.2), "Y property");
            Assert.That(a.Z, Is.EqualTo(3.1), "Z property");
            Assert.That(a.W, Is.EqualTo(0.0), "W property");
            Assert.That(a.IsPoint, Is.False, "is not a point");
            Assert.That(a.IsVector, Is.True, "is a vector");
        }

        [Test]
        public void PointEquals()
        {
            var p = Tuple.Point(4, -4, 3);
            var tpl = new Tuple(4, -4, 3, 1.0);

            Assert.That(p, Is.EqualTo(tpl), "point equals");
        }

        [Test]
        public void VectorEquals()
        {
            var v = Tuple.Vector(4, -4, 3);
            var tpl = new Tuple(4, -4, 3, 0.0);

            Assert.That(v, Is.EqualTo(tpl), "vector equals");
        }

        [Test]
        public void AddPointAndVector()
        {
            var a1 = Tuple.Point(3, -2, 5);
            var a2 = Tuple.Vector(-2, 3, 1);
            var sum = new Tuple(1, 1, 6, 1);
            Assert.That(a1 +a2, Is.EqualTo(sum));
        }

        [Test]
        public void SubtractPoints()
        {
            var p1 = Tuple.Point(3, 2, 1);
            var p2 = Tuple.Point(5, 6, 7);
            var diff = Tuple.Vector(-2, -4, -6);
            Assert.That(p1 - p2, Is.EqualTo(diff));
        }

        [Test]
        public void SubtractPointVector()
        {
            var p = Tuple.Point(3, 2, 1);
            var v = Tuple.Vector(5, 6, 7);
            var diff = Tuple.Point(-2, -4, -6);
            Assert.That(p - v, Is.EqualTo(diff));
        }

        [Test]
        public void SubtractVectors()
        {
            var v1 = Tuple.Vector(3, 2, 1);
            var v2 = Tuple.Vector(5, 6, 7);
            var diff = Tuple.Vector(-2, -4, -6);
            Assert.That(v1 - v2, Is.EqualTo(diff));
        }

        [Test]
        public void VectorFromZeroVector()
        {
            var z = Tuple.Vector(0, 0, 0);
            var v = Tuple.Vector(1, -2, 3);
            var diff = Tuple.Vector(-1, 2, -3);
            Assert.That(z - v, Is.EqualTo(diff));
        }

        [Test]
        public void NegatingTuple()
        {
            var a = new Tuple(1, -2, 3, -4);
            var negA = new Tuple(-1, 2, -3, 4);
            Assert.That(-a, Is.EqualTo(negA));
        }

        [Test]
        public void MultScalar()
        {
            var a = new Tuple(1, -2, 3, -4);
            double scalar = 3.5;
            var mult = new Tuple(3.5, -7, 10.5, -14);
            Assert.That(a * scalar, Is.EqualTo(mult));
        }

        [Test]
        public void MultFraction()
        {
            var a = new Tuple(1, -2, 3, -4);
            double scalar = 0.5;
            var mult = new Tuple(0.5, -1, 1.5, -2);
            Assert.That(a * scalar, Is.EqualTo(mult));
        }

        [Test]
        public void DivideScalar()
        {
            var a = new Tuple(1, -2, 3, -4);
            double scalar = 2;
            var div = new Tuple(0.5, -1, 1.5, -2);
            Assert.That(a / scalar, Is.EqualTo(div));
        }

        [Test]
        public void Magnitude()
        {
            var a = Tuple.Vector(1, 0, 0);
            var b = Tuple.Vector(0, 1, 0);
            var c = Tuple.Vector(0, 0, 1);

            Assert.That(a.Magnitude().AboutEqual(1), Is.True, "Magnitude(" + a + ") = 1.0");
            Assert.That(b.Magnitude().AboutEqual(1), Is.True, "Magnitude(" + b + ") = 1.0");
            Assert.That(c.Magnitude().AboutEqual(1), Is.True, "Magnitude(" + c + ") = 1.0");

            var v1 = Tuple.Vector(1, 2, 3);
            var v2 = Tuple.Vector(-1, -2, -3);
            var mag = Math.Sqrt(14);

            Assert.That(v1.Magnitude().AboutEqual(mag), Is.True, "Magnitude(" + v1 + ") = Sqrt(14)");
            Assert.That(v2.Magnitude().AboutEqual(mag), Is.True, "Magnitude(" + v2 + ") = Sqrt(14)");
        }

        [Test]
        public void Normalize()
        {
            var v1 = Tuple.Vector(4, 0, 0);
            var v2 = Tuple.Vector(1, 2, 3);

            var n1 = Tuple.Vector(1, 0, 0);
            var n2 = Tuple.Vector(0.267261, 0.534522, 0.801783);

            Assert.That(v1.Normalize(), Is.EqualTo(n1), "Normalize(" + v1 + ") = " + n1);
            Assert.That(v2.Normalize(), Is.EqualTo(n2), "Normalize(" + v1 + ") = " + n2);
        }

        [Test]
        public void MagnitudeOfNormalVector()
        {
            var v = Tuple.Vector(1, 2, 3);
            var nrml = v.Normalize();
            Assert.That(nrml.Magnitude().AboutEqual(1.0), Is.True, "magnitude of " + v + " normalized should be 1 but is " + nrml.Magnitude());

            var rand = new Random();
            var randoV = Tuple.Vector(rand.NextDouble(), rand.NextDouble(), rand.NextDouble());
            var randoNormalized = randoV.Normalize();
            Assert.That(randoNormalized.Magnitude().AboutEqual(1.0), Is.True, "magnitude of " + randoV + " normalized should be 1 but is " + randoNormalized.Magnitude());
        }

        [Test]
        public void DotProduct()
        {
            var a = Tuple.Vector(1, 2, 3);
            var b = Tuple.Vector(2, 3, 4);
            Assert.That(a.DotProduct(b).AboutEqual(20), Is.True, "Dot product should have been " + 20);
        }

        [Test]
        public void CrossProduct()
        {
            var a = Tuple.Vector(1, 2, 3);
            var b = Tuple.Vector(2, 3, 4);

            var cross1 = Tuple.Vector(-1, 2, -1);
            var cross2 = Tuple.Vector(1, -2, 1);

            Assert.That(a.CrossProduct(b), Is.EqualTo(cross1), "Cross({0}, {1}) should be {2}", a, b, cross1);
            Assert.That(b.CrossProduct(a), Is.EqualTo(cross2), "Cross({0}, {1}) should be {2}", b, a, cross2);
        }
    }
}
