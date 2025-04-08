using System.Collections.Immutable;

namespace RayTracerChallenge
{
	public readonly record struct Intersection(float T, IIntersectable Object)
	{
		public static ImmutableArray<Intersection> Aggregate(params Intersection[] intersections)
		{
			return [..intersections];
		}
	}
}
