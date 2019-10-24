using System;

namespace RayTracerChallenge
{
    public readonly struct Tuple : IEquatable<Tuple>
    {
        private const double WPoint = 1;
        private const double WVect = 0;
        private readonly Lazy<double> _lazyMagnitude;

        public Tuple(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;

            _lazyMagnitude = new Lazy<double>(() =>
            {
                var sum = Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2) + Math.Pow(w, 2);
                var mag = Math.Sqrt(sum);
                return mag;
            });
        }

        public static Tuple Point(double x, double y, double z)
        {
            var pt = new Tuple(x, y, z, WPoint);
            return pt;
        }

        public static Tuple Vector(double x, double y, double z)
        {
            var pt = new Tuple(x, y, z, WVect);
            return pt;
        }

        public double X { get; }
        public double Y { get; }
        public double Z { get; }
        public double W { get; }

        public bool IsPoint => W.AboutEqual(WPoint);

        public bool IsVector => W.AboutEqual(WVect);

        public Tuple Add(Tuple other)
        {
            if (
                IsPoint && other.IsVector
                ||
                IsVector && other.IsPoint
               )
            {
                return Point(X + other.X, Y + other.Y, Z + other.Z);
            }

            if (this.IsVector && other.IsVector)
            {
                return Vector(X + other.X, Y + other.Y, Z + other.Z);
            }
            throw new ArgumentException("Addition undefined, this IsPoint: {IsPoint} and other IsPoint {other.IsPoint}");
        }

        public Tuple Subtract(Tuple other)
        {
            if (
                IsPoint && other.IsPoint
                ||
                IsVector && other.IsVector
               )
            {
                var v = Vector(X - other.X, Y - other.Y, Z - other.Z);
                return v;
            }

            if (this.IsPoint && other.IsVector)
            {
                var p = Point(X - other.X, Y - other.Y, Z - other.Z);
                return p;
            }
            throw new ArgumentException("Subtraction undefined, this IsVector: {IsVector} and other IsPoint {other.IsPoint}");
        }

        public Tuple Negate()
        {
            var tpl = new Tuple(-X, -Y, -Z, -W);
            return tpl;
        }

        public Tuple Multiply(double scalar)
        {
            var product = new Tuple(X * scalar, Y * scalar, Z * scalar, W * scalar);
            return product;
        }

        public Tuple Divide(double scalar)
        {
            var product = new Tuple(X / scalar, Y / scalar, Z / scalar, W / scalar);
            return product;
        }

        public double Magnitude()
        {
            var mag = _lazyMagnitude.Value;
            return mag;
        }

        public Tuple Normalize()
        {
            var n = this / Magnitude();
            return n;
        }

        /// <summary>
        /// The DotProduct of 2 Normalized vectors is the cosine of the angle between the vectors
        /// </summary>
        /// <param name="otherVector"></param>
        /// <returns></returns>
        public double DotProduct(Tuple otherVector)
        {
            if (this.IsVector == false || otherVector.IsVector == false)
            {
                throw new ArgumentException("DotProduct only defined for 2 vectors.");
            }
            var d = (X * otherVector.X) +
                    (Y * otherVector.Y) +
                    (Z * otherVector.Z) +
                    (W * otherVector.W);
            return d;
        }

        /// <summary>
        /// A new vector perpendicular to this vector and otherVector
        /// </summary>
        /// <param name="otherVector"></param>
        /// <returns></returns>
        public Tuple CrossProduct(Tuple otherVector)
        {
            if (this.IsVector == false || otherVector.IsVector == false)
            {
                throw new ArgumentException("CrossProduct only defined for 2 vectors.");
            }
            var cp = Vector(
                Y * otherVector.Z - Z * otherVector.Y,
                Z * otherVector.X - X * otherVector.Z,
                X * otherVector.Y - Y * otherVector.X);
            return cp;
        }

        public override string ToString()
        {
            return $"(X:{X}, Y:{Y}, Z:{Z}, W:{W})";
        }

        bool IEquatable<Tuple>.Equals(Tuple other)
        {
            return Equals(other);
        }

        public bool Equals(Tuple other)
        {
            return X.AboutEqual(other.X) && Y.AboutEqual(other.Y) && Z.AboutEqual(other.Z) && W.AboutEqual(other.W);
        }

        public override bool Equals(object obj)
        {
            return obj is Tuple other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                hashCode = (hashCode * 397) ^ W.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Tuple left, Tuple right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Tuple left, Tuple right)
        {
            return !left.Equals(right);
        }

        public static Tuple operator +(Tuple left, Tuple right)
        {
            return left.Add(right);
        }

        public static Tuple operator -(Tuple left, Tuple right)
        {
            return left.Subtract(right);
        }

        public static Tuple operator -(Tuple tpl)
        {
            return tpl.Negate();
        }

        public static Tuple operator *(Tuple left, double scalar)
        {
            return left.Multiply(scalar);
        }

        public static Tuple operator /(Tuple left, double scalar)
        {
            return left.Divide(scalar);
        }
    }
}
