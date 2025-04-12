using RayTracerChallenge;

namespace RayTracerTest
{
	public class RayTest
	{
		[Fact]
		public void CreateAndQueryRay()
		{
			var origin = new Point(1, 2, 3);
			var direction = new Vector(4, 5, 6);
			var ray = new Ray(origin, direction);
			Assert.Equal(origin, ray.Origin);
			Assert.Equal(direction, ray.Direction);
		}

		[Fact]
		public void ComputePointFromDistance()
		{
			var ray = new Ray(new Point(2, 3, 4), new Vector(1, 0, 0));
			Assert.Equal(new Point(2, 3, 4), ray.Position(0));
			Assert.Equal(new Point(3, 3, 4), ray.Position(1));
			Assert.Equal(new Point(1, 3, 4), ray.Position(-1));
			Assert.Equal(new Point(4.5, 3, 4), ray.Position(2.5));
		}

		[Fact]
		public void RayIntersectsSphereAtTwoPoints()
		{
			var ray = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
			var sphere = new Sphere();
			var intersections = sphere.Intersect(ray);
			Assert.Equal(2, intersections.Length);
			Assert.Equal(4, intersections[0].T);
			Assert.Equal(6, intersections[1].T);
		}

		[Fact]
		public void RayIntersectsSphereAtTangent()
		{
			var ray = new Ray(new Point(0, 1, -5), new Vector(0, 0, 1));
			var sphere = new Sphere();
			var intersections = sphere.Intersect(ray);
			Assert.Equal(2, intersections.Length);
			Assert.Equal(5, intersections[0].T);
			Assert.Equal(5, intersections[1].T);
		}

		[Fact]
		public void RayMissesSphere()
		{
			var ray = new Ray(new Point(0, 2, -5), new Vector(0, 0, 1));
			var sphere = new Sphere();
			var intersections = sphere.Intersect(ray);
			Assert.Empty(intersections);
		}

		[Fact]
		public void RayOriginatesInsideSphere()
		{
			var ray = new Ray(new Point(0, 0, 0), new Vector(0, 0, 1));
			var sphere = new Sphere();
			var intersections = sphere.Intersect(ray);
			Assert.Equal(2, intersections.Length);
			Assert.Equal(-1, intersections[0].T);
			Assert.Equal(1, intersections[1].T);
		}

		[Fact]
		public void SphereIsBehindRay()
		{
			var ray = new Ray(new Point(0, 0, 5), new Vector(0, 0, 1));
			var sphere = new Sphere();
			var intersections = sphere.Intersect(ray);
			Assert.Equal(2, intersections.Length);
			Assert.Equal(-6, intersections[0].T);
			Assert.Equal(-4, intersections[1].T);
		}

		[Fact]
		public void TranslatingRay()
		{
			var r = new Ray(new Point(1, 2, 3), new Vector(0, 1, 0));
			var m = Transformation.Translation(3, 4, 5);
			var r2 = r.Transform(m);
			Assert.Equal(new Point(4, 6, 8), r2.Origin);
			Assert.Equal(new Vector(0, 1, 0), r2.Direction);
		}

		[Fact]
		public void ScalingRay()
		{
			var r = new Ray(new Point(1, 2, 3), new Vector(0, 1, 0));
			var m = Transformation.Scaling(2, 3, 4);
			var r2 = r.Transform(m);
			Assert.Equal(new Point(2, 6, 12), r2.Origin);
			Assert.Equal(new Vector(0, 3, 0), r2.Direction);
		}
	}
}
