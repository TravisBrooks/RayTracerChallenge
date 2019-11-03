using System;

namespace RayTracerChallenge
{
    public static class Transformation
    {
        public static Matrix Translation(double x, double y, double z)
        {
            var transform = Matrix.Identity();
            transform[0, 3] = x;
            transform[1, 3] = y;
            transform[2, 3] = z;

            return transform;
        }

        public static Matrix Scaling(double x, double y, double z)
        {
            var transform = Matrix.Identity();
            transform[0, 0] = x;
            transform[1, 1] = y;
            transform[2, 2] = z;

            return transform;
        }

        public static Matrix RotationX(double radians)
        {
            var transform = Matrix.Identity();
            var cos = Math.Cos(radians);
            var sin = Math.Sin(radians);
            transform[1, 1] = cos;
            transform[1, 2] = -sin;
            transform[2, 1] = sin;
            transform[2, 2] = cos;
            return transform;
        }

        public static Matrix RotationY(double radians)
        {
            var transform = Matrix.Identity();
            var cos = Math.Cos(radians);
            var sin = Math.Sin(radians);
            transform[0, 0] = cos;
            transform[0, 2] = sin;
            transform[2, 0] = -sin;
            transform[2, 2] = cos;
            return transform;
        }

        public static Matrix RotationZ(double radians)
        {
            var transform = Matrix.Identity();
            var cos = Math.Cos(radians);
            var sin = Math.Sin(radians);
            transform[0, 0] = cos;
            transform[0, 1] = -sin;
            transform[1, 0] = sin;
            transform[1, 1] = cos;
            return transform;
        }

        public static Matrix Shearing
        (
            double x_y,
            double x_z,
            double y_x,
            double y_z,
            double z_x,
            double z_y
        )
        {
            var transform = Matrix.Identity();
            transform[0, 1] = x_y;
            transform[0, 2] = x_z;
            transform[1, 0] = y_x;
            transform[1, 2] = y_z;
            transform[2, 0] = z_x;
            transform[2, 1] = z_y;
            return transform;
        }
    }
}
