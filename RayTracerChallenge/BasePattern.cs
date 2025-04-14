namespace RayTracerChallenge
{
	public abstract class BasePattern
	{
		public Matrix Transform { get; init; } = Matrix.Identity();

		public abstract Color PatternAt(Point point);

		public Color PatternAt(BaseShape obj, Point point)
		{
			var objectPoint = (obj.Transform.Inverse() * point).AssumePoint();
			var patternPoint = (Transform.Inverse() * objectPoint).AssumePoint();
			return PatternAt(patternPoint);
		}
	}
}
