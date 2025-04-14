using System.Collections.Immutable;

namespace RayTracerChallenge;

public class Sphere : BaseShape, IEquatable<Sphere>
{
	public Point Origin { get; init; } = new(0, 0, 0);
	public double Radius { get; init; } = 1.0;

	public override ImmutableArray<Intersection> LocalIntersect(Ray rayInTransformedSpace)
	{
		var sphereToRay = rayInTransformedSpace.Origin - Origin;
		var a = rayInTransformedSpace.Direction.DotProduct(rayInTransformedSpace.Direction);
		var b = 2.0 * rayInTransformedSpace.Direction.DotProduct(sphereToRay);
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

	public Vector NormalAtFoo(Point worldPoint)
	{
		var localPoint = (Transform.Inverse() * worldPoint).AssumePoint();
		var objectNormal = localPoint - Origin;
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

	public override Vector LocalNormalAt(Point localPoint)
	{
		var objectNormal = localPoint - Origin;
		//var worldNormal = Transform.Inverse().Transpose() * objectNormal;
		return objectNormal;
	}

	#region equality stuff

	public bool Equals(Sphere? other)
	{
		if (other is null)
		{
			return false;
		}

		if (ReferenceEquals(this, other))
		{
			return true;
		}

		return Origin.Equals(other.Origin) && Radius.Equals(other.Radius) && Transform.Equals(other.Transform) && Material.Equals(other.Material);
	}

	public override bool Equals(object? obj)
	{
		if (obj is null)
		{
			return false;
		}

		if (ReferenceEquals(this, obj))
		{
			return true;
		}

		if (obj.GetType() != GetType())
		{
			return false;
		}

		return Equals((Sphere)obj);
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Origin, Radius, Transform, Material);
	}

	public static bool operator ==(Sphere? left, Sphere? right)
	{
		return Equals(left, right);
	}

	public static bool operator !=(Sphere? left, Sphere? right)
	{
		return !Equals(left, right);
	}

	#endregion
}