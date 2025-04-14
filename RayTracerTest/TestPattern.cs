using RayTracerChallenge;
using RayTracerChallenge.Patterns;

namespace RayTracerTest
{
	public class TestPattern : BasePattern
	{
		public override Color PatternAt(Point point)
		{
			PassedInPoint = point;
			return new Color(point.X, point.Y, point.Z);
		}

		public Point PassedInPoint { get; set; }
	}
}
