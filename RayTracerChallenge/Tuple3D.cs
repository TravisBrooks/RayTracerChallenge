using System;

namespace RayTracerChallenge
{
    public readonly struct Tuple3D : IEquatable<Tuple3D>
    {
        private const double WPoint = 1;
        private const double WVect = 0;
        private readonly Lazy<double> _lazyMagnitude;

        public Tuple3D(double x, double y, double z, double w)
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

        public static Tuple3D Point(double x, double y, double z)
        {
            var pt = new Tuple3D(x, y, z, WPoint);
            return pt;
        }

        public static Tuple3D Vector(double x, double y, double z)
        {
            var pt = new Tuple3D(x, y, z, WVect);
            return pt;
        }

        public double X { get; }
        public double Y { get; }
        public double Z { get; }
        public double W { get; }

        public bool IsPoint => W.AboutEqual(WPoint);

        public bool IsVector => W.AboutEqual(WVect);

        public double Magnitude()
        {
            var mag = _lazyMagnitude.Value;
            return mag;
        }

        public Tuple3D Normalize()
        {
            var n = this / Magnitude();
            return n;
        }

        public Matrix AsMatrix()
        {
            var m = new Matrix(new[,]
            {
                { X },
                { Y },
                { Z },
                { W },
            });
            return m;
        }

        /// <summary>
        /// The DotProduct of 2 Normalized vectors is the cosine of the angle between the vectors
        /// </summary>
        /// <param name="otherVector"></param>
        /// <returns></returns>
        public double DotProduct(in Tuple3D otherVector)
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
        public Tuple3D CrossProduct(in Tuple3D otherVector)
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

        bool IEquatable<Tuple3D>.Equals(Tuple3D other)
        {
            return Equals(other);
        }

        public bool Equals(Tuple3D other)
        {
            return X.AboutEqual(other.X) && Y.AboutEqual(other.Y) && Z.AboutEqual(other.Z) && W.AboutEqual(other.W);
        }

        public override bool Equals(object obj)
        {
            return obj is Tuple3D other && Equals(other);
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

        public static bool operator ==(in Tuple3D left, in Tuple3D right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(in Tuple3D left, in Tuple3D right)
        {
            return !left.Equals(right);
        }

        public static Tuple3D operator +(in Tuple3D left, in Tuple3D right)
        {
            if (left.IsPoint && right.IsVector
                || left.IsVector && right.IsPoint
            )
            {
                return Point(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
            }

            if (left.IsVector && right.IsVector)
            {
                return Vector(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
            }
            throw new ArgumentException("Addition undefined, this IsPoint: {IsPoint} and other IsPoint {other.IsPoint}");
        }

        public static Tuple3D operator -(in Tuple3D left, in Tuple3D right)
        {
            if (left.IsPoint && right.IsPoint
                || left.IsVector && right.IsVector
            )
            {
                var v = Vector(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
                return v;
            }

            if (left.IsPoint && right.IsVector)
            {
                var p = Point(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
                return p;
            }
            throw new ArgumentException("Subtraction undefined, this IsVector: {IsVector} and other IsPoint {other.IsPoint}");
        }

        public static Tuple3D operator -(in Tuple3D tpl)
        {
            var tpl1 = new Tuple3D(-tpl.X, -tpl.Y, -tpl.Z, -tpl.W);
            return tpl1;
        }

        public static Tuple3D operator *(in Tuple3D left, in double scalar)
        {
            var product = new Tuple3D(left.X * scalar, left.Y * scalar, left.Z * scalar, left.W * scalar);
            return product;
        }

        public static Tuple3D operator /(in Tuple3D left, in double scalar)
        {
            var quotient = new Tuple3D(left.X / scalar, left.Y / scalar, left.Z / scalar, left.W / scalar);
            return quotient;
        }
    }
}
