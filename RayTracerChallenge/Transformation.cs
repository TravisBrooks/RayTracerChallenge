namespace RayTracerChallenge
{
	public static class Transformation
	{
		public static Matrix Translation(double x, double y, double z)
		{
			var m = new Matrix(new[,]
			{
				{ 1, 0, 0, x },
				{ 0, 1, 0, y },
				{ 0, 0, 1, z },
				{ 0, 0, 0, 1 }
			});
			return m;
		}

		public static Matrix Scaling(double x, double y, double z)
		{
			var m = new Matrix(new[,]
			{
				{ x, 0, 0, 0 },
				{ 0, y, 0, 0 },
				{ 0, 0, z, 0 },
				{ 0, 0, 0, 1 }
			});
			return m;
		}

		public static Matrix RotationX(double degreesInRadians)
		{
			var m = new Matrix(new[,]
			{
				{ 1, 0, 0, 0 },
				{ 0, Math.Cos(degreesInRadians), -Math.Sin(degreesInRadians), 0 },
				{ 0, Math.Sin(degreesInRadians), Math.Cos(degreesInRadians), 0 },
				{ 0, 0, 0, 1 }
			});
			return m;
		}

		public static Matrix RotationY(double degreesInRadians)
		{
			var m = new Matrix(new[,]
			{
				{ Math.Cos(degreesInRadians), 0, Math.Sin(degreesInRadians), 0 },
				{ 0, 1, 0, 0 },
				{ -Math.Sin(degreesInRadians), 0, Math.Cos(degreesInRadians), 0 },
				{ 0, 0, 0, 1 }
			});
			return m;
		}

		public static Matrix RotationZ(double degreesInRadians)
		{
			var m = new Matrix(new[,]
			{
				{ Math.Cos(degreesInRadians), -Math.Sin(degreesInRadians), 0, 0 },
				{ Math.Sin(degreesInRadians), Math.Cos(degreesInRadians), 0, 0 },
				{ 0, 0, 1, 0 },
				{ 0, 0, 0, 1 }
			});
			return m;
		}

		public static Matrix Shearing(double xy, double xz, double yx, double yz, double zx, double zy)
		{
			var m = new Matrix(new[,]
			{
				{ 1, xy, xz, 0 },
				{ yx, 1, yz, 0 },
				{ zx, zy, 1, 0 },
				{ 0, 0, 0, 1 }
			});
			return m;
		}

		public static Matrix ViewTransform(Point from, Point to, Vector up)
		{
			var forward = (to - from).Normalize();
			var left = forward.CrossProduct(up.Normalize());
			var trueUp = left.CrossProduct(forward);
			var orientation = new Matrix(new[,]
			{
				{ left.X, left.Y, left.Z, 0 },
				{ trueUp.X, trueUp.Y, trueUp.Z, 0 },
				{ -forward.X, -forward.Y, -forward.Z, 0 },
				{ 0, 0, 0, 1 }
			});
			var vt = orientation * Translation(-from.X, -from.Y, -from.Z);
			return vt;
		}
	}
}
