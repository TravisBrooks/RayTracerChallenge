using RayTracerChallenge;

namespace RayTracerRunner.Chapter02;

public record Projectile(Point Position, Vector Velocity, Color Color)
{
	public void Draw(Canvas canvas)
	{
		var xInt = (int)Math.Round(Position.X);
		var yInt = (int)Math.Round(canvas.Height - Position.Y);
		for (var x = xInt - 1; x < xInt + 3; x++)
		{
			for (var y = yInt - 1; y < yInt + 3; y++)
			{
				if (x >= 0 && y >= 0 && x < canvas.Width && y < canvas.Height)
				{
					canvas[x, y] = Color;
				}
			}
		}
	}
}