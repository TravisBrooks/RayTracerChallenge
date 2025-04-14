using System.Collections.Immutable;

namespace RayTracerChallenge;

public class Plane : BaseShape
{
	public override ImmutableArray<Intersection> LocalIntersect(Ray rayInTransformedSpace)
	{
		if (Math.Abs(rayInTransformedSpace.Direction.Y) < Constants.Epsilon)
		{
			return [];
		}
		var t = -rayInTransformedSpace.Origin.Y / rayInTransformedSpace.Direction.Y;
		return [new Intersection(t, this)];
	}

	public override Vector LocalNormalAt(Point localPoint)
	{
		return new Vector(0, 1, 0);
	}
}