using RayTracerChallenge;

namespace RayTracerRunner.Chapter2
{
	public record Projectile(Point Position, Vector Velocity, Color Color)
	{
		public void Draw(Canvas canvas)
		{
			var xRounded = (int)Math.Ceiling(Position.X);
			var yRounded = (int)Math.Ceiling(canvas.Height - Position.Y);
			for (var x = xRounded - 1; x < xRounded + 3; x++)
			{
				for (var y = yRounded - 1; y < yRounded + 3; y++)
				{
					if (x >= 0 && y >= 0 && x < canvas.Width && y < canvas.Height)
					{
						canvas[x, y] = Color;
					}
				}
			}
		}
	}
}
