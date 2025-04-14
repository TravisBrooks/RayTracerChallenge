using RayTracerChallenge.Patterns;
using System.Collections.Immutable;

namespace RayTracerChallenge;

public abstract class BaseShape
{
	/// <summary>
	/// The transformation matrix of the shape, defaults to the identity matrix
	/// </summary>
	public Matrix Transform { get; init; } = Matrix.Identity();

	/// <summary>
	/// The material of the shape, defaults to Default material.
	/// </summary>
	public Material Material { get; init; } = Material.Default();

	public BasePattern? Pattern { get; init; }

	/// <summary>
	/// Transforms the ray to local space and calls the LocalIntersect method.
	/// </summary>
	/// <param name="ray"></param>
	/// <returns></returns>
	public ImmutableArray<Intersection> Intersect(Ray ray)
	{
		var rayInTransformedSpace = ray.Transform(Transform.Inverse());
		var intersections = LocalIntersect(rayInTransformedSpace);
		return intersections;
	}

	/// <summary>
	/// Calculate the intersection of a ray with the object in its local space.
	/// </summary>
	/// <param name="rayInTransformedSpace"></param>
	/// <returns></returns>
	public abstract ImmutableArray<Intersection> LocalIntersect(Ray rayInTransformedSpace);

	/// <summary>
	/// Calculate the normal of a point on the object (presumably the point where a ray intersects the object).
	/// </summary>
	/// <param name="worldPoint"></param>
	/// <returns></returns>
	public Vector NormalAt(Point worldPoint)
	{
		var localPoint = (Transform.Inverse() * worldPoint).AssumePoint();
		var objectNormal = LocalNormalAt(localPoint);
		var worldNormal = Transform.Inverse().Transpose() * objectNormal;
		// The book explains this in chpt 6, it's a hack to get a vector in the point case.
		// The more correct slower way would be to take a submatrix of the Transform
		var normal = worldNormal.HandleResult(
			vector => vector,
			point => new Vector(point.X, point.Y, point.Z)
		);
		return normal.Normalize();
	}

	public abstract Vector LocalNormalAt(Point localPoint);
}