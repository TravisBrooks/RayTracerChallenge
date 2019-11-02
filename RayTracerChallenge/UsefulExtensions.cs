using System;

namespace RayTracerChallenge
{
    public static class UsefulExtensions
    {
        public static bool AboutEqual(this double lhs, double rhs)
        {
            const double tolerance = 0.00001;
            var aboutEqual = Math.Abs(lhs - rhs) < tolerance;
            return aboutEqual;
        }

        private const double ToRadiansConversion = Math.PI / 180;
        public static double ToRadians(this double degrees)
        {
            return ToRadiansConversion * degrees;
        }
    }
}
