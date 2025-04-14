namespace RayTracerChallenge;

public class StripePattern(Color a, Color b) : BasePattern
{
	public Color A { get; } = a;
	public Color B { get; } = b;

	public override Color PatternAt(Point point)
	{
		var floorOfX = (int)Math.Floor(point.X);
		return floorOfX % 2 == 0 ? A : B;
	}
}