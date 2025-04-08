using System.Collections.Immutable;

namespace RayTracerChallenge
{
	public record struct Sphere(Point Origin, float Radius)
	{
		public Matrix Transform { get; set; } = Matrix.Identity();
		public Material Material { get; set; } = Material.Default();

		public static Sphere Unit()
		{
			return new Sphere(new Point(0, 0, 0), 1f);
		}

		public ImmutableArray<Intersection> Intersect(Ray ray)
		{
			var transRay = ray.Transform(Transform.Inverse());
			var sphereToRay = transRay.Origin - Origin;
			var a = transRay.Direction.DotProduct(transRay.Direction);
			var b = 2f * transRay.Direction.DotProduct(sphereToRay);
			var c = sphereToRay.DotProduct(sphereToRay) - 1;
			var discriminant = b * b - 4f * a * c;

			// no intersection
			if (discriminant < 0)
			{
				return ImmutableArray<Intersection>.Empty;
			}

			var sqrtDiscriminant = MathF.Sqrt(discriminant);
			var t1 = (-b - sqrtDiscriminant) / (2f * a);
			var t2 = (-b + sqrtDiscriminant) / (2f * a);

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
	}
}
