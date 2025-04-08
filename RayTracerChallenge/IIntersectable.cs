using System.Collections.Immutable;

namespace RayTracerChallenge
{
	public interface IIntersectable
	{
		ImmutableArray<Intersection> Intersect(Ray ray);
		Vector NormalAt(Point worldPoint);
		Material Material { get; }
	}
}
