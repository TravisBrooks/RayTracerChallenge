namespace RayTracerChallenge
{
	/// <summary>
	/// This is my attempt to come up with a minimal way to handle an algebraic type that
	/// can be either a Vector or a Point.
	/// </summary>
	public class MaybeVectorOrPoint
	{
		private readonly ITuple3D _tuple;
		private readonly bool _isVector;

		public MaybeVectorOrPoint(Vector v)
		{
			_isVector = true;
			_tuple = v;
		}

		public MaybeVectorOrPoint(Point p)
		{
			_isVector = false;
			_tuple = p;
		}

		/// <summary>
		/// Do something to either a vector or a point. You have to provide a handler for each type
		/// but only 1 will actually be called.
		/// </summary>
		/// <param name="vectorHandler">Do something if the value is a vector</param>
		/// <param name="pointHandler">Do something if the value is a point</param>
		public void HandleResult(
			Action<Vector> vectorHandler,
			Action<Point> pointHandler)
		{
			if (_isVector)
			{
				vectorHandler((Vector)_tuple);
			}
			else
			{
				pointHandler((Point)_tuple);
			}
		}

		public Point AssumePoint()
		{
			if (_isVector)
			{
				throw new InvalidOperationException("This is a vector, not a point");
			}
			return (Point)_tuple;
		}

		public Vector AssumeVector()
		{
			if (!_isVector)
			{
				throw new InvalidOperationException("This is a point, not a vector");
			}
			return (Vector)_tuple;
		}

	}
}
