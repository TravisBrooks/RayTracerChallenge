using RayTracerChallenge;

namespace RayTracerTest
{
	public class TransformationTest
	{
		[Fact]
		public void MultiplyingByTranslationMatrix()
		{
			var transform = Transformation.Translation(5, -3, 2);
			var p = new Point(-3, 4, 5);
			var expected = new Point(2, 1, 7);
			var maybeVorP = transform * p;
			maybeVorP.HandleResult(
				vector => Assert.Fail($"Was not expecting a vector result: {vector}"),
				point => Assert.Equal(expected, point)
			);
		}

		[Fact]
		public void MultiplyByInverseOfTranslationMatrix()
		{
			var transform = Transformation.Translation(5, -3, 2);
			var inverse = transform.Inverse();
			var p = new Point(-3, 4, 5);
			var expected = new Point(-8, 7, 3);
			var maybeVorP = inverse * p;
			maybeVorP.HandleResult(
				vector => Assert.Fail($"Was not expecting a vector result: {vector}"),
				point => Assert.Equal(expected, point)
			);
		}

		[Fact]
		public void TranslationDoesNotAffectVectors()
		{
			var transform = Transformation.Translation(5, -3, 2);
			var v = new Vector(-3, 4, 5);
			var expected = new Vector(-3, 4, 5);
			var maybeVorP = transform * v;
			maybeVorP.HandleResult(
				vector => Assert.Equal(expected, vector),
				point => Assert.Fail($"Was not expecting a point result: {point}")
			);
		}

		[Fact]
		public void ScalingPoint()
		{
			var transform = Transformation.Scaling(2, 3, 4);
			var p = new Point(-4, 6, 8);
			var expected = new Point(-8, 18, 32);
			var maybeVorP = transform * p;
			maybeVorP.HandleResult(
				vector => Assert.Fail($"Was not expecting a vector result: {vector}"),
				point => Assert.Equal(expected, point)
			);
		}

		[Fact]
		public void ScalingVector()
		{
			var transform = Transformation.Scaling(2, 3, 4);
			var v = new Vector(-4, 6, 8);
			var expected = new Vector(-8, 18, 32);
			var maybeVorP = transform * v;
			maybeVorP.HandleResult(
				vector => Assert.Equal(expected, vector),
				point => Assert.Fail($"Was not expecting a point result: {point}")
			);
		}

		[Fact]
		public void ScalingVectorByInverse()
		{
			var transform = Transformation.Scaling(2, 3, 4);
			var inverse = transform.Inverse();
			var v = new Vector(-4, 6, 8);
			var expected = new Vector(-2, 2, 2);
			var maybeVorP = inverse * v;
			maybeVorP.HandleResult(
				vector => Assert.Equal(expected, vector),
				point => Assert.Fail($"Was not expecting a point result: {point}")
			);
		}

		[Fact]
		public void ReflectionPoint()
		{
			var transform = Transformation.Scaling(-1, 1, 1);
			var p = new Point(2, 3, 4);
			var expected = new Point(-2, 3, 4);
			var maybeVorP = transform * p;
			maybeVorP.HandleResult(
				vector => Assert.Fail($"Was not expecting a vector result: {vector}"),
				point => Assert.Equal(expected, point)
			);
		}

		[Fact]
		public void RotatingPointAroundXAxis()
		{
			var p = new Point(0, 1, 0);
			var halfQuarter = Transformation.RotationX(45f.ToRadians());
			var fullQuarter = Transformation.RotationX(90f.ToRadians());

			var expectedHalfQuarter = new Point(0, MathF.Sqrt(2) / 2, MathF.Sqrt(2) / 2);
			var maybeVorPHalfQuarter = halfQuarter * p;
			maybeVorPHalfQuarter.HandleResult(
				vector => Assert.Fail($"Was not expecting a vector result: {vector}"),
				point => Assert.Equal(expectedHalfQuarter, point)
			);

			var expectedFullQuarter = new Point(0, 0, 1);
			var maybeVorPFullQuarter = fullQuarter * p;
			maybeVorPFullQuarter.HandleResult(
				vector => Assert.Fail($"Was not expecting a vector result: {vector}"),
				point => Assert.Equal(expectedFullQuarter, point)
			);
		}

		[Fact]
		public void InverseOfRotationGoesCounterClockWise()
		{
			var p = new Point(0, 1, 0);
			var halfQuarter = Transformation.RotationX(45f.ToRadians());
			var inverse = halfQuarter.Inverse();
			var expected = new Point(0, MathF.Sqrt(2) / 2, -MathF.Sqrt(2) / 2);
			var maybeVorP = inverse * p;
			maybeVorP.HandleResult(
				vector => Assert.Fail($"Was not expecting a vector result: {vector}"),
				point => Assert.Equal(expected, point)
			);
		}

		[Fact]
		public void RotatingPointAroundYAxis()
		{
			var p = new Point(0, 0, 1);
			var halfQuarter = Transformation.RotationY(45f.ToRadians());
			var fullQuarter = Transformation.RotationY(90f.ToRadians());

			var expectedHalfQuarter = new Point(MathF.Sqrt(2) / 2, 0, MathF.Sqrt(2) / 2);
			var maybeVorPHalfQuarter = halfQuarter * p;
			maybeVorPHalfQuarter.HandleResult(
				vector => Assert.Fail($"Was not expecting a vector result: {vector}"),
				point => Assert.Equal(expectedHalfQuarter, point)
			);
			
			var expectedFullQuarter = new Point(1, 0, 0);
			var maybeVorPFullQuarter = fullQuarter * p;
			maybeVorPFullQuarter.HandleResult(
				vector => Assert.Fail($"Was not expecting a vector result: {vector}"),
				point => Assert.Equal(expectedFullQuarter, point)
			);
		}

		[Fact]
		public void RotatingPointAroundZAxis()
		{
			var p = new Point(0, 1, 0);
			var halfQuarter = Transformation.RotationZ(45f.ToRadians());
			var fullQuarter = Transformation.RotationZ(90f.ToRadians());

			var expectedHalfQuarter = new Point(-MathF.Sqrt(2) / 2, MathF.Sqrt(2) / 2, 0);
			var maybeVorPHalfQuarter = halfQuarter * p;
			maybeVorPHalfQuarter.HandleResult(
				vector => Assert.Fail($"Was not expecting a vector result: {vector}"),
				point => Assert.Equal(expectedHalfQuarter, point)
			);

			var expectedFullQuarter = new Point(-1, 0, 0);
			var maybeVorPFullQuarter = fullQuarter * p;
			maybeVorPFullQuarter.HandleResult(
				vector => Assert.Fail($"Was not expecting a vector result: {vector}"),
				point => Assert.Equal(expectedFullQuarter, point)
			);
		}

		[Fact]
		public void ShearingTransformationMovesXinProportionToY()
		{
			var transform = Transformation.Shearing(1.0f, 0f, 0f, 0f, 0f, 0f);
			var p = new Point(2, 3, 4);
			var expected = new Point(5, 3, 4);
			var maybeVorP = transform * p;
			maybeVorP.HandleResult(
				vector => Assert.Fail($"Was not expecting a vector result: {vector}"),
				point => Assert.Equal(expected, point)
			);
		}

		[Fact]
		public void ShearingTransformationMovesXInProportionToZ()
		{
			var transform = Transformation.Shearing(0f, 1.0f, 0f, 0f, 0f, 0f);
			var p = new Point(2, 3, 4);
			var expected = new Point(6, 3, 4);
			var maybeVorP = transform * p;
			maybeVorP.HandleResult(
				vector => Assert.Fail($"Was not expecting a vector result: {vector}"),
				point => Assert.Equal(expected, point)
			);
		}

		[Fact]
		public void ShearingTransformationMovesYInProportionToX()
		{
			var transform = Transformation.Shearing(0f, 0f, 1.0f, 0f, 0f, 0f);
			var p = new Point(2, 3, 4);
			var expected = new Point(2, 5, 4);
			var maybeVorP = transform * p;
			maybeVorP.HandleResult(
				vector => Assert.Fail($"Was not expecting a vector result: {vector}"),
				point => Assert.Equal(expected, point)
			);
		}

		[Fact]
		public void ShearingTransformationMovesYInProportionToZ()
		{
			var transform = Transformation.Shearing(0f, 0f, 0f, 1.0f, 0f, 0f);
			var p = new Point(2, 3, 4);
			var expected = new Point(2, 7, 4);
			var maybeVorP = transform * p;
			maybeVorP.HandleResult(
				vector => Assert.Fail($"Was not expecting a vector result: {vector}"),
				point => Assert.Equal(expected, point)
			);
		}

		[Fact]
		public void ShearingTransformationMovesZInProportionToX()
		{
			var transform = Transformation.Shearing(0f, 0f, 0f, 0f, 1.0f, 0f);
			var p = new Point(2, 3, 4);
			var expected = new Point(2, 3, 6);
			var maybeVorP = transform * p;
			maybeVorP.HandleResult(
				vector => Assert.Fail($"Was not expecting a vector result: {vector}"),
				point => Assert.Equal(expected, point)
			);
		}

		[Fact]
		public void ShearingTransformationMovesZInProportionToY()
		{
			var transform = Transformation.Shearing(0f, 0f, 0f, 0f, 0f, 1.0f);
			var p = new Point(2, 3, 4);
			var expected = new Point(2, 3, 7);
			var maybeVorP = transform * p;
			maybeVorP.HandleResult(
				vector => Assert.Fail($"Was not expecting a vector result: {vector}"),
				point => Assert.Equal(expected, point)
			);
		}

		[Fact]
		public void IndividualTransformationsAreAppliedInSequence()
		{
			var p = new Point(1, 0, 1);
			var a = Transformation.RotationX(90f.ToRadians());
			var b = Transformation.Scaling(5, 5, 5);
			var c = Transformation.Translation(10, 5, 7);
			// rotation first
			var expectedRotation = new Point(1, -1, 0);
			var rotationMaybeVorP = a * p;
			Point rotatedPt = default;
			rotationMaybeVorP.HandleResult(
				vector => Assert.Fail($"Was not expecting a vector result: {vector}"),
				point =>
				{
					rotatedPt = point;
					Assert.Equal(expectedRotation, point);
				});
			// then apply scaling
			var expectedScaling = new Point(5, -5, 0);
			var scalingMaybeVorP = b * rotatedPt;
			Point scaledPt = default;
			scalingMaybeVorP.HandleResult(
				vector => Assert.Fail($"Was not expecting a vector result: {vector}"),
				point =>
				{
					scaledPt = point;
					Assert.Equal(expectedScaling, point);
				});
			// then apply translation
			var expectedTranslation = new Point(15, 0, 7);
			var translationMaybeVorP = c * scaledPt;
			translationMaybeVorP.HandleResult(
				vector => Assert.Fail($"Was not expecting a vector result: {vector}"),
				point => Assert.Equal(expectedTranslation, point)
			);
		}

		[Fact]
		public void ChainedTransformationsAreAppliedInReverseOrder()
		{
			var p = new Point(1, 0, 1);
			var a = Transformation.RotationX(90f.ToRadians());
			var b = Transformation.Scaling(5, 5, 5);
			var c = Transformation.Translation(10, 5, 7);
			var t = c * b * a;
			var expected = new Point(15, 0, 7);
			var maybeVorP = t * p;
			maybeVorP.HandleResult(
				vector => Assert.Fail($"Was not expecting a vector result: {vector}"),
				point => Assert.Equal(expected, point)
			);
		}

		[Fact]
		public void ViewTransformationForDefaultOrientation()
		{
			var from = new Point(0, 0, 0);
			var to = new Point(0, 0, -1);
			var up = new Vector(0, 1, 0);
			var t = Transformation.ViewTransform(from, to, up);
			var expected = Matrix.Identity();
			Assert.Equal(expected, t);
		}

		[Fact]
		public void ViewTransformationLookingInPositiveZDirection()
		{
			var from = new Point(0, 0, 0);
			var to = new Point(0, 0, 1);
			var up = new Vector(0, 1, 0);
			var t = Transformation.ViewTransform(from, to, up);
			var expected = Transformation.Scaling(-1, 1, -1);
			Assert.Equal(expected, t);
		}

		[Fact]
		public void ViewTransformationMovesTheWorld()
		{
			var from = new Point(0, 0, 8);
			var to = new Point(0, 0, 0);
			var up = new Vector(0, 1, 0);
			var t = Transformation.ViewTransform(from, to, up);
			var expected = Transformation.Translation(0, 0, -8);
			Assert.Equal(expected, t);
		}

		[Fact]
		public void ViewTransformationArbitraryViewTransformation()
		{
			var from = new Point(1, 3, 2);
			var to = new Point(4, -2, 8);
			var up = new Vector(1, 1, 0);
			var t = Transformation.ViewTransform(from, to, up);
			var expected = new Matrix(new[,]
			{
				{ -0.50709f, 0.50709f, 0.67612f, -2.36643f },
				{ 0.76772f, 0.60609f, 0.12122f, -2.82843f },
				{ -0.35857f, 0.59761f, -0.71714f, 0 },
				{ 0, 0, 0, 1 }
			});
			Assert.Equal(expected, t);
		}
	}
}
