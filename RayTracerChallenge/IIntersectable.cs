using System.Collections.Immutable;

namespace RayTracerChallenge
{
	public interface IIntersectable
	{
		/// <summary>
		/// Calculate if the ray intersects the object.
		/// </summary>
		/// <param name="ray"></param>
		/// <returns></returns>
		ImmutableArray<Intersection> Intersect(Ray ray);

		/// <summary>
		/// Calculate the normal of a point on the object (presumably the point where a ray intersects the object).
		/// </summary>
		/// <param name="worldPoint"></param>
		/// <returns></returns>
		Vector NormalAt(Point worldPoint);

		/// <summary>
		/// The material of the object that can be intersected.
		/// </summary>
		Material Material { get; }
	}
}
