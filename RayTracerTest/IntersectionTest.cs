using RayTracerChallenge;

namespace RayTracerTest;

public class IntersectionTest
{
	[Fact]
	public void IntersectionEncapsulatesTandObject()
	{
		var sphere = new Sphere();
		var i = new Intersection(3.5, sphere);
		Assert.Equal(3.5, i.T);
		Assert.Equal(sphere, i.Object);
	}

	[Fact]
	public void AggregatingIntersections()
	{
		var sphere = new Sphere();
		var i1 = new Intersection(1, sphere);
		var i2 = new Intersection(2, sphere);
		var intersections = Intersection.Aggregate(i1, i2);
		Assert.Equal(2, intersections.Length);
		Assert.Equal(i1, intersections[0]);
		Assert.Equal(i2, intersections[1]);
	}

	[Fact]
	public void HitWhenAllIntersectionsPositive()
	{
		var sphere = new Sphere();
		var i1 = new Intersection(1, sphere);
		var i2 = new Intersection(2, sphere);
		var intersections = Intersection.Aggregate(i1, i2);
		var hit = intersections.Hit();
		Assert.Equal(i1, hit);
	}

	[Fact]
	public void HitWhenSomeIntersectionsNegative()
	{
		var sphere = new Sphere();
		var i1 = new Intersection(-1, sphere);
		var i2 = new Intersection(1, sphere);
		var intersections = Intersection.Aggregate(i1, i2);
		var hit = intersections.Hit();
		Assert.Equal(i2, hit);
	}

	[Fact]
	public void HitWhenAllIntersectionsNegative()
	{
		var sphere = new Sphere();
		var i1 = new Intersection(-2, sphere);
		var i2 = new Intersection(-1, sphere);
		var intersections = Intersection.Aggregate(i1, i2);
		var hit = intersections.Hit();
		Assert.Null(hit);
	}

	[Fact]
	public void HitIsAlwaysLowestNonNegativeIntersection()
	{
		var sphere = new Sphere();
		var i1 = new Intersection(5, sphere);
		var i2 = new Intersection(7, sphere);
		var i3 = new Intersection(-3, sphere);
		var i4 = new Intersection(2, sphere);
		var intersections = Intersection.Aggregate(i1, i2, i3, i4);
		var hit = intersections.Hit();
		Assert.Equal(i4, hit);
	}

	[Fact]
	public void PrecomputingStateOfIntersection()
	{
		var ray = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		var sphere = new Sphere();
		var intersection = new Intersection(4, sphere);
		var computation = intersection.PrepareComputation(ray);
		Assert.Equal(intersection.T, computation.T);
		Assert.Equal(intersection.Object, computation.Object);
		Assert.Equal(new Point(0, 0, -1), computation.Point);
		Assert.Equal(new Vector(0, 0, -1), computation.EyeVector);
		Assert.Equal(new Vector(0, 0, -1), computation.NormalVector);
	}

	[Fact]
	public void PrepareComputationHitOnOutside()
	{
		var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		var shape = new Sphere();
		var i = new Intersection(4, shape);
		var comps = i.PrepareComputation(r);
		Assert.False(comps.Inside);
	}

	[Fact]
	public void PrepareComputationHitOnInside()
	{
		var r = new Ray(new Point(0, 0, 0), new Vector(0, 0, 1));
		var shape = new Sphere();
		var i = new Intersection(1, shape);
		var comps = i.PrepareComputation(r);
		Assert.Equal(new Point(0, 0, 1), comps.Point);
		Assert.Equal(new Vector(0, 0, -1), comps.EyeVector);
		Assert.Equal(new Vector(0, 0, -1), comps.NormalVector);
		Assert.True(comps.Inside);
	}

	[Fact]
	public void TheHitShouldOffsetThePoint()
	{
		var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		var shape = new Sphere
		{
			Transform = Transformation.Translation(0, 0, 1)
		};
		var i = new Intersection(5, shape);
		var comps = i.PrepareComputation(r);
		Assert.True(comps.OverPoint.Z < -Constants.Epsilon/2);
		Assert.True(comps.Point.Z > comps.OverPoint.Z);
	}

	[Fact]
	public void PrecomputingTheReflectionVector()
	{
		var shape = new Plane();
		var r = new Ray(new Point(0, 1, -1), new Vector(0, -Math.Sqrt(2) / 2.0, Math.Sqrt(2) / 2.0));
		var i = new Intersection(Math.Sqrt(2), shape);
		var comps = i.PrepareComputation(r);
		Assert.Equal(new Vector(0, Math.Sqrt(2) / 2.0, Math.Sqrt(2) / 2.0), comps.ReflectionVector);
	}

	[Fact]
	public void FindingN1andN2ForVariousIntersections()
	{
		var sphereA = Sphere.GlassySphere(refractiveIndex: 1.5, transform: Transformation.Scaling(2, 2, 2));
		var sphereB = Sphere.GlassySphere(refractiveIndex: 2, transform: Transformation.Scaling(0, 0, -0.25));
		var sphereC = Sphere.GlassySphere(refractiveIndex: 2.5, transform: Transformation.Scaling(0, 0, 0.25));
		var ray = new Ray(new Point(0, 0, -4), new Vector(0, 0, 1));
		var i1 = new Intersection(2, sphereA);
		var i2 = new Intersection(2.75, sphereB);
		var i3 = new Intersection(3.25, sphereC);
		var i4 = new Intersection(4.75, sphereB);
		var i5 = new Intersection(5.25, sphereC);
		var i6 = new Intersection(6, sphereA);
		var xs = Intersection.Aggregate(i1, i2, i3, i4, i5, i6);
		var expectedN1 = new double[] { 1.0, 1.5, 2.0, 2.5, 2.5, 1.5 };
		var expectedN2 = new double[] { 1.5, 2.0, 2.5, 2.5, 1.5, 1.0 };
		for (var i=0;i<6; i++)
		{
			var comps = xs[i].PrepareComputation(ray, xs);
			Assert.Equal(expectedN1[i], comps.N1);
			Assert.Equal(expectedN2[i], comps.N2);
		}
	}

	[Fact]
	public void TheUnderPointIsOffsetBelowTheSurface()
	{
		var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		var shape = Sphere.GlassySphere(transform: Transformation.Translation(0, 0, 1));
		var i = new Intersection(5, shape);
		var xs = Intersection.Aggregate(i);
		var comps = i.PrepareComputation(r, xs);
		Assert.True(comps.UnderPoint.Z > Constants.Epsilon / 2.0);
		Assert.True(comps.Point.Z < comps.UnderPoint.Z);
	}

	[Fact]
	public void SchlickApproximationUnderTotalInternalReflection()
	{
		var shape = Sphere.GlassySphere();
		var r = new Ray(new Point(0, 0, Math.Sqrt(2) / 2.0), new Vector(0, 1, 0));
		var i1 = new Intersection(-Math.Sqrt(2) / 2.0, shape);
		var i2 = new Intersection(Math.Sqrt(2) / 2.0, shape);
		var xs = Intersection.Aggregate(i1, i2);
		var comps = i2.PrepareComputation(r, xs);
		var reflectance = comps.Schlick();
		Assert.Equal(1.0, reflectance);
	}

	[Fact]
	public void SchlickApproximationWithPerpendicularViewingAngle()
	{
		var shape = Sphere.GlassySphere();
		var r = new Ray(new Point(0, 0, 0), new Vector(0, 1, 0));
		var i1 = new Intersection(-1, shape);
		var i2 = new Intersection(1, shape);
		var xs = Intersection.Aggregate(i1, i2);
		var comps = i2.PrepareComputation(r, xs);
		var reflectance = comps.Schlick();
		Assert.True(0.04.AboutEqual(reflectance));
	}

	[Fact]
	public void SchlickApproximationWithSmallAngleAndN2GreaterThanN1()
	{
		var shape = Sphere.GlassySphere();
		var r = new Ray(new Point(0, 0.99, -2), new Vector(0, 0, 1));
		var i1 = new Intersection(1.8589, shape);
		var xs = Intersection.Aggregate(i1);
		var comps = i1.PrepareComputation(r, xs);
		var reflectance = comps.Schlick();
		Assert.True(0.48873.AboutEqual(reflectance));
	}
}