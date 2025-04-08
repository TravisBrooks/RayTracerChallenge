using RayTracerChallenge;

namespace RayTracerTest
{
	public class SphereTest
	{
		[Fact]
		public void IntersectSetsTheObject()
		{
			var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
			var s = Sphere.Unit();
			var xs = s.Intersect(r);
			Assert.Equal(2, xs.Length);
			Assert.Equal(s, xs[0].Object);
			Assert.Equal(s, xs[1].Object);
		}

		[Fact]
		public void SphereHasDefaultTransform()
		{
			var s = Sphere.Unit();
			Assert.Equal(Matrix.Identity(), s.Transform);
		}

		[Fact]
		public void ChangingSpheresTransform()
		{
			var s = Sphere.Unit();
			var t = Transformation.Translation(2, 3, 4);
			s.Transform = t;
			Assert.Equal(t, s.Transform);
		}

		[Fact]
		public void IntersectingScaledSphereWithRay()
		{
			var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
			var s = Sphere.Unit();
			s.Transform = Transformation.Scaling(2, 2, 2);
			var xs = s.Intersect(r);
			Assert.Equal(2, xs.Length);
			Assert.Equal(3f, xs[0].T);
			Assert.Equal(7f, xs[1].T);
		}

		[Fact]
		public void IntersectingTranslatedSphereWithRay()
		{
			var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
			var s = Sphere.Unit();
			s.Transform = Transformation.Translation(5, 0, 0);
			var xs = s.Intersect(r);
			Assert.Empty(xs);
		}

		[Fact]
		public void NormalOnTheX_Axis()
		{
			var s = Sphere.Unit();
			var n = s.NormalAt(new Point(1, 0, 0));
			Assert.Equal(new Vector(1, 0, 0), n);
		}

		[Fact]
		public void NormalOnTheY_Axis()
		{
			var s = Sphere.Unit();
			var n = s.NormalAt(new Point(0, 1, 0));
			Assert.Equal(new Vector(0, 1, 0), n);
		}

		[Fact]
		public void NormalOnTheZ_Axis()
		{
			var s = Sphere.Unit();
			var n = s.NormalAt(new Point(0, 0, 1));
			Assert.Equal(new Vector(0, 0, 1), n);
		}

		[Fact]
		public void NormalOnNonAxialPoint()
		{
			var s = Sphere.Unit();
			var sqrt3div3 = MathF.Sqrt(3) / 3;
			var n = s.NormalAt(new Point(sqrt3div3, sqrt3div3, sqrt3div3));
			Assert.Equal(new Vector(sqrt3div3, sqrt3div3, sqrt3div3), n);
		}

		[Fact]
		public void NormalIsNormalizedVector()
		{
			var s = Sphere.Unit();
			var sqrt3div3 = MathF.Sqrt(3) / 3;
			var n = s.NormalAt(new Point(sqrt3div3, sqrt3div3, sqrt3div3));
			Assert.Equal(n.Normalize(), n);
		}

		[Fact]
		public void NormalOnTranslatedSphere()
		{
			var s = Sphere.Unit();
			s.Transform = Transformation.Translation(0, 1, 0);
			var n = s.NormalAt(new Point(0, 1.70711f, -0.70711f));
			Assert.Equal(new Vector(0, 0.70711f, -0.70711f), n);
		}

		[Fact]
		public void NormalOnTransformedSphere()
		{
			var s = Sphere.Unit();
			var m = Transformation.Scaling(1, 0.5f, 1) * Transformation.RotationZ(MathF.PI / 5);
			s.Transform = m;
			var n = s.NormalAt(new Point(0, MathF.Sqrt(2) / 2, -MathF.Sqrt(2) / 2));
			Assert.Equal(new Vector(0, 0.97014f, -0.24254f), n);
		}

		[Fact]
		public void SphereHasDefaultMaterial()
		{
			var s = Sphere.Unit();
			Assert.Equal(Material.Default(), s.Material);
		}

		[Fact]
		public void SphereCanBeAssignedMaterial()
		{
			var s = Sphere.Unit();
			var m = Material.Default() with {Ambient = 1f};
			s.Material = m;
			Assert.Equal(m, s.Material);
		}
	}
}
