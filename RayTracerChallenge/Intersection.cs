using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracerChallenge
{
	public readonly record struct Intersection(float T, object Object)
	{
		public static ImmutableArray<Intersection> Aggregate(params Intersection[] intersections)
		{
			return [..intersections];
		}
	}
}
