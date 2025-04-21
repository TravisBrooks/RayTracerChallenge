using System.Collections.Immutable;

namespace RayTracerChallenge
{
	public class Cube : BaseShape
	{
		public override ImmutableArray<Intersection> LocalIntersect(Ray rayInTransformedSpace)
		{
			var (xMin, xMax) = CheckAxis(rayInTransformedSpace.Origin.X, rayInTransformedSpace.Direction.X);
			var (yMin, yMax) = CheckAxis(rayInTransformedSpace.Origin.Y, rayInTransformedSpace.Direction.Y);
			var (zMin, zMax) = CheckAxis(rayInTransformedSpace.Origin.Z, rayInTransformedSpace.Direction.Z);
			var tMin = Math.Max(Math.Max(xMin, yMin), zMin);
			var tMax = Math.Min(Math.Min(xMax, yMax), zMax);

			if (tMin > tMax)
			{
				return ImmutableArray<Intersection>.Empty;
			}

			var i1 = new Intersection(tMin, this);
			var i2 = new Intersection(tMax, this);
			return Intersection.Aggregate(i1, i2);
		}

		public override Vector LocalNormalAt(Point localPoint)
		{
			var maxC = Math.Max(Math.Abs(localPoint.X), Math.Max(Math.Abs(localPoint.Y), Math.Abs(localPoint.Z)));
			if (maxC.AboutEqual(Math.Abs(localPoint.X)))
			{
				return new Vector(localPoint.X, 0, 0);
			}
			if (maxC.AboutEqual(Math.Abs(localPoint.Y)))
			{
				return new Vector(0, localPoint.Y, 0);
			}
			return new Vector(0, 0, localPoint.Z);
		}

		private static (double min, double max) CheckAxis(double origin, double direction)
		{
			var minNum = -1.0 - origin;
			var maxNum = 1.0 - origin;
			double tMin, tMax;
			if (Math.Abs(direction) >= Constants.Epsilon)
			{
				tMin = minNum / direction;
				tMax = maxNum / direction;
			}
			else
			{
				tMin = minNum * double.PositiveInfinity;
				tMax = maxNum * double.PositiveInfinity;
			}
			if (tMin > tMax)
			{
				(tMin, tMax) = (tMax, tMin);
			}
			return (tMin, tMax);
		}
	}
}
