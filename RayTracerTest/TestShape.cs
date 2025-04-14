using System.Collections.Immutable;
using RayTracerChallenge;

namespace RayTracerTest;

internal class TestShape : BaseShape
{
	public override ImmutableArray<Intersection> LocalIntersect(Ray rayInTransformedSpace)
	{
		RayInTransformedSpace = rayInTransformedSpace;
		return [];
	}

	public override Vector LocalNormalAt(Point localPoint)
	{
		LocalPoint = localPoint;
		return new Vector(localPoint.X, localPoint.Y, localPoint.Z);
	}

	public Ray RayInTransformedSpace { get; private set; }
	public Point LocalPoint { get; private set; }
}