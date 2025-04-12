using RayTracerChallenge;

namespace RayTracerRunner.Chapter4;

public record ClockPoint(Point Point, Color Color)
{
	public void Draw(Canvas canvas)
	{
		var xInt = (int)Math.Round(Point.X);
		// inverting the y coordinate to draw on canvas
		var yInt = (int)Math.Round(canvas.Height - Point.Y);
		var rectSize = 15;
		for (var x = 0; x < rectSize; x++)
		{
			var ptX = xInt + x;
			if (ptX >= 0 && ptX < canvas.Width - 1)
			{
				for (var y = 0; y < rectSize; y++)
				{
					var ptY = yInt + y;
					if (ptY >= 0 && ptY < canvas.Height - 1)
					{
						canvas[ptX, ptY] = Color;
					}
				}
			}
		}
	}
}