namespace RayTracerChallenge
{
	public interface ITuple3D
	{
		float X { get; }
		float Y { get; }
		float Z { get; }
		float W { get; }

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
