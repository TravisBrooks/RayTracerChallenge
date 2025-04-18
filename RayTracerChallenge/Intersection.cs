﻿using System.Collections.Immutable;

namespace RayTracerChallenge;

public readonly record struct Intersection(double T, BaseShape Object)
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
		var overPoint = point + normal * Constants.Epsilon;
		return new PhongComputation(T, Object, point, eye, normal, inside, overPoint);
	}
}