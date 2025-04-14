namespace RayTracerChallenge.Patterns
{
	public class CheckersPattern(Color a, Color b) : BasePattern
	{
		public Color A { get; } = a;
		public Color B { get; } = b;

		public override Color PatternAt(Point point)
		{
			var sum = Math.Floor(point.X) + Math.Floor(point.Y) + Math.Floor(point.Z);
			return sum % 2 == 0 ? A : B;
		}
	}
}
