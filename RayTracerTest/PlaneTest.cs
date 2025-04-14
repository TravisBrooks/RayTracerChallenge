using RayTracerChallenge;

namespace RayTracerTest;

public class PlaneTest
{
	[Fact]
	public void TheNormalOfPlaneIsConstantEverywhere()
	{
		var p = new Plane();
		var n1 = p.LocalNormalAt(new Point(0, 0, 0));
		var n2 = p.LocalNormalAt(new Point(10, 0, -10));
		var n3 = p.LocalNormalAt(new Point(-5, 0, 150));
		Assert.Equal(new Vector(0, 1, 0), n1);
		Assert.Equal(new Vector(0, 1, 0), n2);
		Assert.Equal(new Vector(0, 1, 0), n3);
	}

	[Fact]
	public void IntersectWithRayParallelToPlane()
	{
		var p = new Plane();
		var r = new Ray(new Point(0, 10, 0), new Vector(0, 0, 1));
		var xs = p.LocalIntersect(r);
		Assert.Empty(xs);
	}

	[Fact]
	public void IntersectWithCoplanarRay()
	{
		var p = new Plane();
		var r = new Ray(new Point(0, 0, 0), new Vector(0, 0, 1));
		var xs = p.LocalIntersect(r);
		Assert.Empty(xs);
	}

	[Fact]
	public void RayIntersectingPlaneFromAbove()
	{
		var p = new Plane();
		var r = new Ray(new Point(0, 1, 0), new Vector(0, -1, 0));
		var xs = p.LocalIntersect(r);
		Assert.Single(xs);
		Assert.Equal(1, xs[0].T);
		Assert.Equal(p, xs[0].Object);
	}

	[Fact]
	public void RayIntersectingPlaneFromBelow()
	{
		var p = new Plane();
		var r = new Ray(new Point(0, -1, 0), new Vector(0, 1, 0));
		var xs = p.LocalIntersect(r);
		Assert.Single(xs);
		Assert.Equal(1, xs[0].T);
		Assert.Equal(p, xs[0].Object);
	}
}