namespace RayTracerChallenge
{
	public readonly record struct Vector(double X, double Y, double Z) : ITuple3D
	{
		public double W => 0;

		public bool Equals(Vector other)
		{
			return X.AboutEqual(other.X) &&
			       Y.AboutEqual(other.Y) &&
			       Z.AboutEqual(other.Z);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(X, Y, Z);
		}

		public static Point operator +(Vector v1, Point v2)
		{
			var p = new Point(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
			return p;
		}

		public static Vector operator +(Vector v1, Vector v2)
		{
			var v = new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
			return v;
		}

		public static Vector operator -(Vector v1, Vector v2)
		{
			var v = new Vector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
			return v;
		}

		public static Vector operator -(Vector v)
		{
			var vr = new Vector(-v.X, -v.Y, -v.Z);
			return vr;
		}

		public static Vector operator *(Vector left, double scalar)
		{
			var v = new Vector(left.X * scalar, left.Y * scalar, left.Z * scalar);
			return v;
		}

		public static Vector operator /(Vector left, double scalar)
		{
			var v = new Vector(left.X / scalar, left.Y / scalar, left.Z / scalar);
			return v;
		}

		public double Magnitude()
		{
			var sum = Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2);
			var mag = Math.Sqrt(sum);
			return mag;
		}

		public Vector Normalize()
		{
			var magnitude = Magnitude();
			var n = this / magnitude;
			return n;
		}

		public double DotProduct(Vector v)
		{
			var dot = X * v.X + Y * v.Y + Z * v.Z;
			return dot;
		}

		/// <summary>
		/// Produces a vector perpendicular to both "this" and the v vector.
		/// </summary>
		/// <param name="v">The vector to cross with.</param>
		/// <returns>A new vector that is perpendicular to both vectors.</returns>
		public Vector CrossProduct(Vector v)
		{
			var cp = new Vector(
				Y * v.Z - Z * v.Y,
				Z * v.X - X * v.Z,
				X * v.Y - Y * v.X);
			return cp;
		}

		public Vector Reflect(Vector normal)
		{
			var r = this - normal * 2.0 * DotProduct(normal);
			return r;
		}
	}
}