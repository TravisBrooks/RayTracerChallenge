
namespace RayTracerChallenge
{
	public readonly record struct Point(float X, float Y, float Z) : ITuple3D
	{
		public float W => 1f;

		public bool Equals(Point other)
		{
			return X.AboutEqual(other.X) &&
			       Y.AboutEqual(other.Y) &&
			       Z.AboutEqual(other.Z);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(X, Y, Z);
		}

		public static Point operator +(Point p, Vector v)
		{
			var newP = new Point(p.X + v.X, p.Y + v.Y, p.Z + v.Z);
			return newP;
		}

		public static Point operator -(Point p, Vector v)
		{
			var newP = new Point(p.X - v.X, p.Y - v.Y, p.Z - v.Z);
			return newP;
		}

		public static Vector operator -(Point p1, Point p2)
		{
			var v = new Vector(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
			return v;
		}
	}
}
