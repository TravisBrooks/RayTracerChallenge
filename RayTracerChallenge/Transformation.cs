namespace RayTracerChallenge
{
	public static class Transformation
	{
		public static Matrix Translation(float x, float y, float z)
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

		public static Matrix Scaling(float x, float y, float z)
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

		public static Matrix RotationX(float degreesInRadians)
		{
			var m = new Matrix(new[,]
			{
				{ 1, 0, 0, 0 },
				{ 0, MathF.Cos(degreesInRadians), -MathF.Sin(degreesInRadians), 0 },
				{ 0, MathF.Sin(degreesInRadians), MathF.Cos(degreesInRadians), 0 },
				{ 0, 0, 0, 1 }
			});
			return m;
		}

		public static Matrix RotationY(float degreesInRadians)
		{
			var m = new Matrix(new[,]
			{
				{ MathF.Cos(degreesInRadians), 0, MathF.Sin(degreesInRadians), 0 },
				{ 0, 1, 0, 0 },
				{ -MathF.Sin(degreesInRadians), 0, MathF.Cos(degreesInRadians), 0 },
				{ 0, 0, 0, 1 }
			});
			return m;
		}

		public static Matrix RotationZ(float degreesInRadians)
		{
			var m = new Matrix(new[,]
			{
				{ MathF.Cos(degreesInRadians), -MathF.Sin(degreesInRadians), 0, 0 },
				{ MathF.Sin(degreesInRadians), MathF.Cos(degreesInRadians), 0, 0 },
				{ 0, 0, 1, 0 },
				{ 0, 0, 0, 1 }
			});
			return m;
		}

		public static Matrix Shearing(float xy, float xz, float yx, float yz, float zx, float zy)
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
	}
}
