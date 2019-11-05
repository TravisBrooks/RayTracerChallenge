using System;

namespace RayTracerChallenge
{
    public struct Ray : IEquatable<Ray>
    {
        public Ray(Tuple3D originPoint, Tuple3D directionVector)
        {
            OriginPoint = originPoint;
            DirectionVector = directionVector;
        }

        public Tuple3D OriginPoint { get; }
        public Tuple3D DirectionVector { get; }

        public Tuple3D Position(double time)
        {
            var p = OriginPoint + DirectionVector * time;
            return p;
        }

        public bool Equals(Ray other)
        {
            return OriginPoint.Equals(other.OriginPoint) && DirectionVector.Equals(other.DirectionVector);
        }

        public override bool Equals(object obj)
        {
            return obj is Ray other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (OriginPoint.GetHashCode() * 397) ^ DirectionVector.GetHashCode();
            }
        }

        public static bool operator ==(in Ray left, in Ray right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(in Ray left, in Ray right)
        {
            return !left.Equals(right);
        }
    }
}
