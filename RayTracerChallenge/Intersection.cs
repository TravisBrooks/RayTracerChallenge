using System.Collections.Immutable;

namespace RayTracerChallenge
{
	public readonly record struct Intersection(float T, IIntersectable Object)
	{
		public static ImmutableArray<Intersection> Aggregate(params Intersection[] intersections)
		{
			return [..intersections];
		}

		public PhongComputation PrepareComputation(Ray ray)
		{
			var point = ray.Position(T);
			var normal = Object.NormalAt(point);
			var eye = -ray.Direction;
			var inside = false;
			if (normal.DotProduct(eye) < 0)
			{
				inside = true;
				normal = -normal;
			}
			return new PhongComputation(T, Object, point, eye, normal, inside);
		}
	}
}
