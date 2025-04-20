using RayTracerChallenge;

namespace RayTracerRunner.Chapter05;

public class Chapter5Demo : DemoRun
{
	protected override Canvas RunCanvasRender()
	{
		var canvasPixels = 500u;
		var canvas = new Canvas(canvasPixels, canvasPixels);

		var backgroundColor = Color.FromRgb(0, 0, 0);
		var shadowColor = Color.FromRgb(255, 0, 0);
		var otherColor = Color.FromRgb(0, 0, 255);
		canvas.SetAllPixels(backgroundColor);

		var cameraPos = new Point(0, 0, -5);
		var wallZ = 13.0;
		var wallSize = 7.0;
		var pixelSize = wallSize / canvasPixels;
		var sphere1 = new Sphere();
		var sphere2 = new Sphere
		{
			Transform = Transformation.Scaling(0.5, 1, 1) * Transformation.Shearing(2, 0, 0, 0, 0, 0)
		};

		var translation = -(wallSize * 0.5);
		var xRange = Enumerable.Range(0, (int)canvasPixels);
		Parallel.ForEach(xRange, x =>
		{
			for (var y = 0; y < canvasPixels; y++)
			{
				var directionVector = new Vector(x * pixelSize + translation, y * pixelSize + translation, wallZ).Normalize();
				var ray = new Ray(cameraPos, directionVector);
				var hit1 = sphere1.Intersect(ray).Hit();
				var hit2 = sphere2.Intersect(ray).Hit();

				if (hit1 is not null)
				{
					canvas[x, y] += shadowColor;
				}

				if (hit2 is not null)
				{
					canvas[x, y] += otherColor;
				}
			}
		});
			
		return canvas;
	}
}