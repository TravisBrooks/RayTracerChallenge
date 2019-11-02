using System;
using NUnit.Framework;
using RayTracerChallenge;
using Tuple = RayTracerChallenge.Tuple;

namespace Tests
{
    [TestFixture]
    public class TransformationTest
    {
        [Test]
        public void MultiplyByTranslationMatrix()
        {
            var transform = Transformation.Translation(5, -3, 2);
            var p = Tuple.Point(-3, 4, 5);
            var expected = Tuple.Point(2, 1, 7);
            var actual = transform * p;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void MultiplyByInverseOfTranslationMatrix()
        {
            var transform = Transformation.Translation(5, -3, 2);
            var inv = transform.Inverse();
            var p = Tuple.Point(-3, 4, 5);
            var expected = Tuple.Point(-8, 7, 3);
            var actual = inv * p;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TranslationDoesNotAffectVectors()
        {
            var transform = Transformation.Translation(5, -3, 2);
            var v = Tuple.Vector(-3, 4, 5);
            var actual = transform * v;
            Assert.That(actual, Is.EqualTo(v));
        }

        [Test]
        public void ScalingPoint()
        {
            var transform = Transformation.Scaling(2, 3, 4);
            var p = Tuple.Point(-4, 6, 8);
            var expected = Tuple.Point(-8, 18, 32);
            var actual = transform * p;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ScalingVector()
        {
            var transform = Transformation.Scaling(2, 3, 4);
            var v = Tuple.Vector(-4, 6, 8);
            var expected = Tuple.Vector(-8, 18, 32);
            var actual = transform * v;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void MultiplyByInverseOfScalingMatrix()
        {
            var transform = Transformation.Scaling(2, 3, 4);
            var inv = transform.Inverse();
            var v = Tuple.Vector(-4, 6, 8);
            var expected = Tuple.Vector(-2, 2, 2);
            var actual = inv * v;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ReflectionIsScalingByNegativeValue()
        {
            var transform = Transformation.Scaling(-1, 1, 1);
            var p = Tuple.Point(2, 3, 4);
            var expected = Tuple.Point(-2, 3, 4);
            var actual = transform * p;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void RotatePointAroundXAxis()
        {
            var p = Tuple.Point(0, 1, 0);
            var halfQuarter = Transformation.RotationX(45d.ToRadians());
            var fullQuarter = Transformation.RotationX(90d.ToRadians());

            var expectedHalfQtr = Tuple.Point(0, Math.Sqrt(2)/2.0, Math.Sqrt(2) / 2.0);
            var expectedFullQtr = Tuple.Point(0, 0, 1);

            var actualHalfQtr = halfQuarter * p;
            var actualFullQtr = fullQuarter * p;

            Assert.That(actualHalfQtr, Is.EqualTo(expectedHalfQtr), "half qtr");
            Assert.That(actualFullQtr, Is.EqualTo(expectedFullQtr), "full qtr");
        }

        [Test]
        public void InverseOfXRotationGoesInOppositeDirection()
        {
            var p = Tuple.Point(0, 1, 0);
            var halfQuarter = Transformation.RotationX(45d.ToRadians());
            var inv = halfQuarter.Inverse();
            var expected = Tuple.Point(0, Math.Sqrt(2) / 2.0, -Math.Sqrt(2) / 2.0);
            var actual = inv * p;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void RotatePointAroundYAxis()
        {
            var p = Tuple.Point(0, 0, 1);
            var halfQuarter = Transformation.RotationY(45d.ToRadians());
            var fullQuarter = Transformation.RotationY(90d.ToRadians());

            var expectedHalfQtr = Tuple.Point(Math.Sqrt(2) / 2.0, 0, Math.Sqrt(2) / 2.0);
            var expectedFullQtr = Tuple.Point(1, 0, 0);

            var actualHalfQtr = halfQuarter * p;
            var actualFullQtr = fullQuarter * p;

            Assert.That(actualHalfQtr, Is.EqualTo(expectedHalfQtr), "half qtr");
            Assert.That(actualFullQtr, Is.EqualTo(expectedFullQtr), "full qtr");
        }

        [Test]
        public void RotatePointAroundZAxis()
        {
            var p = Tuple.Point(0, 1, 0);
            var halfQuarter = Transformation.RotationZ(45d.ToRadians());
            var fullQuarter = Transformation.RotationZ(90d.ToRadians());

            var expectedHalfQtr = Tuple.Point(-Math.Sqrt(2) / 2.0, Math.Sqrt(2) / 2.0, 0);
            var expectedFullQtr = Tuple.Point(-1, 0, 0);

            var actualHalfQtr = halfQuarter * p;
            var actualFullQtr = fullQuarter * p;

            Assert.That(actualHalfQtr, Is.EqualTo(expectedHalfQtr), "half qtr");
            Assert.That(actualFullQtr, Is.EqualTo(expectedFullQtr), "full qtr");
        }
    }
}
