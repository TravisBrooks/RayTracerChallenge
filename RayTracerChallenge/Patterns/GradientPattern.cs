namespace RayTracerChallenge.Patterns
{
	public class GradientPattern(Color a, Color b) : BasePattern
	{
		public Color A { get; } = a;
		public Color B { get; } = b;

		public override Color PatternAt(Point point)
		{
			var distance = B - A;
			var fraction = point.X - Math.Floor(point.X);
			var c = A + distance * fraction;
			return c;
		}
	}
}
