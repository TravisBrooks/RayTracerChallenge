using System.Collections.Immutable;

namespace RayTracerChallenge;

public record struct Sphere(Point Origin, double Radius) : IIntersectable
{
	public Sphere() : this(new Point(0, 0, 0), 1.0)
	{
	}

	public Matrix Transform { get; init; } = Matrix.Identity();
	public Material Material { get; set; } = Material.Default();

	public ImmutableArray<Intersection> Intersect(Ray ray)
	{
		var transRay = ray.Transform(Transform.Inverse());
		var sphereToRay = transRay.Origin - Origin;
		var a = transRay.Direction.DotProduct(transRay.Direction);
		var b = 2.0 * transRay.Direction.DotProduct(sphereToRay);
		var c = sphereToRay.DotProduct(sphereToRay) - 1;
		var discriminant = b * b - 4.0 * a * c;

		// no intersection
		if (discriminant < 0)
		{
			return ImmutableArray<Intersection>.Empty;
		}

		var sqrtDiscriminant = Math.Sqrt(discriminant);
		var t1 = (-b - sqrtDiscriminant) / (2.0 * a);
		var t2 = (-b + sqrtDiscriminant) / (2.0 * a);

		if (t1 > t2)
		{
			(t1, t2) = (t2, t1);
		}

		return [new Intersection(t1, this), new Intersection(t2, this)];
	}

	public Vector NormalAt(Point worldPoint)
	{
		var objectPoint = (Transform.Inverse() * worldPoint).AssumePoint();
		var objectNormal = objectPoint - Origin;
		var worldNormal = Transform.Inverse().Transpose() * objectNormal;
		Vector normal = default;
		// The book explains this in chpt 6, it's a hack to get a vector in the point case.
		// The more correct slower way would be to take a submatrix of the Transform
		worldNormal.HandleResult(
			vector => normal = vector,
			point => normal = new Vector(point.X, point.Y, point.Z)
		);
		return normal.Normalize();
	}

	#region custom equality for mutable properties

	public readonly bool Equals(Sphere other)
	{
		return Transform.Equals(other.Transform) &&
		       Material.Equals(other.Material) &&
		       Origin.Equals(other.Origin) &&
		       Radius.Equals(other.Radius);
	}

	public readonly override int GetHashCode()
	{
		return HashCode.Combine(Transform, Material, Origin, Radius);
	}
	#endregion
}