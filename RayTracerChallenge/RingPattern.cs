namespace RayTracerChallenge
{
	public class RingPattern(Color a, Color b) : BasePattern
	{
		public Color A { get; } = a;
		public Color B { get; } = b;

		public override Color PatternAt(Point point)
		{
			var floorOfX = (int)Math.Floor(Math.Sqrt(point.X * point.X + point.Z * point.Z));
			return floorOfX % 2 == 0 ? A : B;
		}
	}
}
