using System.Collections.Immutable;

namespace RayTracerChallenge;

public static class IntersectionExtension
{
	public static Intersection? Hit(this ImmutableArray<Intersection> intersections)
	{
		var hit = intersections
			.Where(i => i.T > 0)
			.OrderBy(i => i.T)
			// cast is needed to allow a null return for a non hit (Intersection is a struct value type)
			.Cast<Intersection?>()
			.FirstOrDefault();
		return hit;
	}
}