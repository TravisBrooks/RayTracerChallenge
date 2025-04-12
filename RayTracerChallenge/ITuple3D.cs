namespace RayTracerChallenge
{
	public interface ITuple3D
	{
		double X { get; }
		double Y { get; }
		double Z { get; }
		double W { get; }

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
	}
}
