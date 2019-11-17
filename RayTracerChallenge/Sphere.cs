using System;

namespace RayTracerChallenge
{
    public class Sphere
    {
        public Sphere()
        {
            Transform = Matrix.Identity();
        }

        public Matrix Transform { get; set; }

        public IntersectionList Intersect(in Ray ray)
        {
            var rayTransformed = ray.Transform(Transform.Inverse());
            var sphereToRay = rayTransformed.OriginPoint - Tuple3D.Point(0, 0, 0);
            var a = rayTransformed.DirectionVector.DotProduct(rayTransformed.DirectionVector);
            var b = 2 * rayTransformed.DirectionVector.DotProduct(sphereToRay);
            var c = sphereToRay.DotProduct(sphereToRay) - 1;
            var disc = Math.Pow(b, 2) - 4 * a * c;
            var discriminant = disc;
            if (discriminant < 0)
            {
                return new IntersectionList();
            }

            var t1 = (-b - Math.Sqrt(discriminant)) / (2 * a);
            var t2 = (-b + Math.Sqrt(discriminant)) / (2 * a);
            var list = new IntersectionList
            (
                new Intersection(t1, this),
                new Intersection(t2, this)
            );
            return list;
        }

        public Tuple3D NormalAt(Tuple3D worldPoint)
        {
            var inverse = Transform.Inverse();
            var objectPt = inverse * worldPoint;
            var objectNormal = objectPt - Tuple3D.Point(0, 0, 0);
            var worldNormal = inverse.Transpose() * objectNormal;
            worldNormal  = Tuple3D.Vector(worldNormal.X, worldNormal.Y, worldNormal.Z);
            var nrml = worldNormal.Normalize();
            return nrml;
        }
    }
}
