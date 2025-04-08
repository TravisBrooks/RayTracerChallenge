using RayTracerChallenge;

namespace RayTracerTest
{
	public class IntersectionTest
	{
		[Fact]
		public void IntersectionEncapsulatesTandObject()
		{
			var sphere = new Sphere();
			var i = new Intersection(3.5f, sphere);
			Assert.Equal(3.5f, i.T);
			Assert.Equal(sphere, i.Object);
		}

		[Fact]
		public void AggregatingIntersections()
		{
			var sphere = new Sphere();
			var i1 = new Intersection(1f, sphere);
			var i2 = new Intersection(2f, sphere);
			var intersections = Intersection.Aggregate(i1, i2);
			Assert.Equal(2, intersections.Length);
			Assert.Equal(i1, intersections[0]);
			Assert.Equal(i2, intersections[1]);
		}

		[Fact]
		public void HitWhenAllIntersectionsPositive()
		{
			var sphere = new Sphere();
			var i1 = new Intersection(1f, sphere);
			var i2 = new Intersection(2f, sphere);
			var intersections = Intersection.Aggregate(i1, i2);
			var hit = intersections.Hit();
			Assert.Equal(i1, hit);
		}

		[Fact]
		public void HitWhenSomeIntersectionsNegative()
		{
			var sphere = new Sphere();
			var i1 = new Intersection(-1f, sphere);
			var i2 = new Intersection(1f, sphere);
			var intersections = Intersection.Aggregate(i1, i2);
			var hit = intersections.Hit();
			Assert.Equal(i2, hit);
		}

		[Fact]
		public void HitWhenAllIntersectionsNegative()
		{
			var sphere = new Sphere();
			var i1 = new Intersection(-2f, sphere);
			var i2 = new Intersection(-1f, sphere);
			var intersections = Intersection.Aggregate(i1, i2);
			var hit = intersections.Hit();
			Assert.Null(hit);
		}

		[Fact]
		public void HitIsAlwaysLowestNonNegativeIntersection()
		{
			var sphere = new Sphere();
			var i1 = new Intersection(5f, sphere);
			var i2 = new Intersection(7f, sphere);
			var i3 = new Intersection(-3f, sphere);
			var i4 = new Intersection(2f, sphere);
			var intersections = Intersection.Aggregate(i1, i2, i3, i4);
			var hit = intersections.Hit();
			Assert.Equal(i4, hit);
		}
	}
}
