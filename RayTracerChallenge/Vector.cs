namespace RayTracerChallenge
{
	public readonly record struct Vector(float X, float Y, float Z) : ITuple3D
	{
		public float W => 0f;

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

		public static Vector operator *(Vector left, float scalar)
		{
			var v = new Vector(left.X * scalar, left.Y * scalar, left.Z * scalar);
			return v;
		}

		public static Vector operator /(Vector left, float scalar)
		{
			var v = new Vector(left.X / scalar, left.Y / scalar, left.Z / scalar);
			return v;
		}

		public float Magnitude()
		{
			var sum = Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2);
			var mag = (float)Math.Sqrt(sum);
			return mag;
		}

		public Vector Normalize()
		{
			var magnitude = Magnitude();
			var n = this / magnitude;
			return n;
		}

		public float DotProduct(Vector v)
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
	}
}