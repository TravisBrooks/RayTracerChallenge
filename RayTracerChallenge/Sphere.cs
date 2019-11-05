using System;
using System.Collections.Generic;

namespace RayTracerChallenge
{
    public struct Sphere
    {
        public IReadOnlyList<double> Intersect(in Ray ray)
        {
            var sphereToRay = ray.OriginPoint - Tuple3D.Point(0, 0, 0);
            var a = ray.DirectionVector.DotProduct(ray.DirectionVector);
            var b = 2 * ray.DirectionVector.DotProduct(sphereToRay);
            var c = sphereToRay.DotProduct(sphereToRay) - 1;
            var disc = Math.Pow(b, 2) - 4 * a * c;
            var discriminant = disc;
            if (discriminant < 0)
            {
                return new double[0];
            }

            var t1 = (-b - Math.Sqrt(discriminant)) / (2 * a);
            var t2 = (-b + Math.Sqrt(discriminant)) / (2 * a);
            return new[]{t1, t2};
        }
    }
}
