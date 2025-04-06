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
	}
}
