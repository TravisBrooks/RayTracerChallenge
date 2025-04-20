using System.Collections.Immutable;

namespace RayTracerChallenge;

public readonly record struct Intersection(double T, BaseShape Object)
{
	public static ImmutableArray<Intersection> Aggregate(params Intersection[] intersections)
	{
		return [..intersections];
	}

	public PhongComputation PrepareComputation(Ray ray, ImmutableArray<Intersection> xs)
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
		var overPoint = point + normal * Constants.Epsilon;
		var underPoint = point - normal * Constants.Epsilon;
		var reflection = ray.Direction.Reflect(normal);

		var containers = new List<BaseShape>();
		double n1 = 0, n2 = 0;
		foreach (var i in xs)
		{
			if (i == this)
			{
				if (containers.Count == 0)
				{
					n1 = 1.0;
				}
				else
				{
					n1 = containers.Last().Material.RefractiveIndex;
				}
			}

			var indexOf = containers.IndexOf(i.Object);
			if (indexOf >= 0)
			{
				containers.RemoveAt(indexOf);
			}
			else
			{
				containers.Add(i.Object);
			}

			if (i == this)
			{
				if (containers.Count == 0)
				{
					n2 = 1.0;
				}
				else
				{
					n2 = containers.Last().Material.RefractiveIndex;
				}
			}
		}

		return new PhongComputation(T, Object, point, eye, normal, inside, overPoint, reflection, n1, n2, underPoint);
	}

	public PhongComputation PrepareComputation(Ray ray)
	{
		return PrepareComputation(ray, [this]);
	}
}