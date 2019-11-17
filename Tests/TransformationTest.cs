using System;
using NUnit.Framework;
using RayTracerChallenge;

namespace Tests
{
    [TestFixture]
    public class TransformationTest
    {
        [Test]
        public void MultiplyByTranslationMatrix()
        {
            var transform = Transformation.Translation(5, -3, 2);
            var p = Tuple3D.Point(-3, 4, 5);
            var expected = Tuple3D.Point(2, 1, 7);
            var actual = transform * p;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void MultiplyByInverseOfTranslationMatrix()
        {
            var transform = Transformation.Translation(5, -3, 2);
            var inv = transform.Inverse();
            var p = Tuple3D.Point(-3, 4, 5);
            var expected = Tuple3D.Point(-8, 7, 3);
            var actual = inv * p;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TranslationDoesNotAffectVectors()
        {
            var transform = Transformation.Translation(5, -3, 2);
            var v = Tuple3D.Vector(-3, 4, 5);
            var actual = transform * v;
            Assert.That(actual, Is.EqualTo(v));
        }

        [Test]
        public void ScalingPoint()
        {
            var transform = Transformation.Scaling(2, 3, 4);
            var p = Tuple3D.Point(-4, 6, 8);
            var expected = Tuple3D.Point(-8, 18, 32);
            var actual = transform * p;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ScalingVector()
        {
            var transform = Transformation.Scaling(2, 3, 4);
            var v = Tuple3D.Vector(-4, 6, 8);
            var expected = Tuple3D.Vector(-8, 18, 32);
            var actual = transform * v;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void MultiplyByInverseOfScalingMatrix()
        {
            var transform = Transformation.Scaling(2, 3, 4);
            var inv = transform.Inverse();
            var v = Tuple3D.Vector(-4, 6, 8);
            var expected = Tuple3D.Vector(-2, 2, 2);
            var actual = inv * v;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ReflectionIsScalingByNegativeValue()
        {
            var transform = Transformation.Scaling(-1, 1, 1);
            var p = Tuple3D.Point(2, 3, 4);
            var expected = Tuple3D.Point(-2, 3, 4);
            var actual = transform * p;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void RotatePointAroundXAxis()
        {
            var p = Tuple3D.Point(0, 1, 0);
            var halfQuarter = Transformation.RotationX(45d.ToRadians());
            var fullQuarter = Transformation.RotationX(90d.ToRadians());

            var expectedHalfQtr = Tuple3D.Point(0, Math.Sqrt(2)/2.0, Math.Sqrt(2) / 2.0);
            var expectedFullQtr = Tuple3D.Point(0, 0, 1);

            var actualHalfQtr = halfQuarter * p;
            var actualFullQtr = fullQuarter * p;

            Assert.That(actualHalfQtr, Is.EqualTo(expectedHalfQtr), "half qtr");
            Assert.That(actualFullQtr, Is.EqualTo(expectedFullQtr), "full qtr");
        }

        [Test]
        public void InverseOfXRotationGoesInOppositeDirection()
        {
            var p = Tuple3D.Point(0, 1, 0);
            var halfQuarter = Transformation.RotationX(45d.ToRadians());
            var inv = halfQuarter.Inverse();
            var expected = Tuple3D.Point(0, Math.Sqrt(2) / 2.0, -Math.Sqrt(2) / 2.0);
            var actual = inv * p;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void RotatePointAroundYAxis()
        {
            var p = Tuple3D.Point(0, 0, 1);
            var halfQuarter = Transformation.RotationY(45d.ToRadians());
            var fullQuarter = Transformation.RotationY(90d.ToRadians());

            var expectedHalfQtr = Tuple3D.Point(Math.Sqrt(2) / 2.0, 0, Math.Sqrt(2) / 2.0);
            var expectedFullQtr = Tuple3D.Point(1, 0, 0);

            var actualHalfQtr = halfQuarter * p;
            var actualFullQtr = fullQuarter * p;

            Assert.That(actualHalfQtr, Is.EqualTo(expectedHalfQtr), "half qtr");
            Assert.That(actualFullQtr, Is.EqualTo(expectedFullQtr), "full qtr");
        }

        [Test]
        public void RotatePointAroundZAxis()
        {
            var p = Tuple3D.Point(0, 1, 0);
            var halfQuarter = Transformation.RotationZ(45d.ToRadians());
            var fullQuarter = Transformation.RotationZ(90d.ToRadians());

            var expectedHalfQtr = Tuple3D.Point(-Math.Sqrt(2) / 2.0, Math.Sqrt(2) / 2.0, 0);
            var expectedFullQtr = Tuple3D.Point(-1, 0, 0);

            var actualHalfQtr = halfQuarter * p;
            var actualFullQtr = fullQuarter * p;

            Assert.That(actualHalfQtr, Is.EqualTo(expectedHalfQtr), "half qtr");
            Assert.That(actualFullQtr, Is.EqualTo(expectedFullQtr), "full qtr");
        }

        [Test]
        public void ShearingMovesXProportionY()
        {
            var transform = Transformation.Shearing(1, 0, 0, 0, 0, 0);
            var p = Tuple3D.Point(2, 3, 4);
            var expected = Tuple3D.Point(5, 3, 4);
            var actual = transform * p;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ShearingMovesXProportionZ()
        {
            var transform = Transformation.Shearing(0, 1, 0, 0, 0, 0);
            var p = Tuple3D.Point(2, 3, 4);
            var expected = Tuple3D.Point(6, 3, 4);
            var actual = transform * p;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ShearingMovesYProportionX()
        {
            var transform = Transformation.Shearing(0, 0, 1, 0, 0, 0);
            var p = Tuple3D.Point(2, 3, 4);
            var expected = Tuple3D.Point(2, 5, 4);
            var actual = transform * p;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ShearingMovesYProportionZ()
        {
            var transform = Transformation.Shearing(0, 0, 0, 1, 0, 0);
            var p = Tuple3D.Point(2, 3, 4);
            var expected = Tuple3D.Point(2, 7, 4);
            var actual = transform * p;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ShearingMovesZProportionX()
        {
            var transform = Transformation.Shearing(0, 0, 0, 0, 1, 0);
            var p = Tuple3D.Point(2, 3, 4);
            var expected = Tuple3D.Point(2, 3, 6);
            var actual = transform * p;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ShearingMovesZProportionY()
        {
            var transform = Transformation.Shearing(0, 0, 0, 0, 0, 1);
            var p = Tuple3D.Point(2, 3, 4);
            var expected = Tuple3D.Point(2, 3, 7);
            var actual = transform * p;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TransformationsAppliedInSequence()
        {
            var A = Transformation.RotationX(Math.PI / 2);
            var B = Transformation.Scaling(5, 5, 5);
            var C = Transformation.Translation(10, 5, 7);
            var p = Tuple3D.Point(1, 0, 1);
            var p2 = A * p;
            Assert.That(p2, Is.EqualTo(Tuple3D.Point(1, -1, 0)), "p2");
            var p3 = B * p2;
            Assert.That(p3, Is.EqualTo(Tuple3D.Point(5, -5, 0)), "p3");
            var p4 = C * p3;
            Assert.That(p4, Is.EqualTo(Tuple3D.Point(15, 0, 7)), "p4");
        }

        [Test]
        public void ChainedTransformations()
        {
            var transform = Transformation.Translation(10, 5, 7)
                                          .Scale(5, 5, 5)
                                          .RotateX(Math.PI / 2.0);
            var p = Tuple3D.Point(1, 0, 1);
            var expected = Tuple3D.Point(15, 0, 7);
            var actual = transform * p;
            Assert.That(actual, Is.EqualTo(expected));
        }

    }
}
