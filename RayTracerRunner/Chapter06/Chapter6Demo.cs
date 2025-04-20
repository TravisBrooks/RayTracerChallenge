using RayTracerChallenge;

namespace RayTracerRunner.Chapter06;

internal class Chapter6Demo : DemoRun
{
	protected override Canvas RunCanvasRender()
	{
		var canvasPixels = 500u;
		var canvas = new Canvas(canvasPixels, canvasPixels);

		var backgroundColor = Color.FromRgb(0, 0, 0);
		canvas.SetAllPixels(backgroundColor);

		var cameraPos = new Point(0, 0, -5);
		var wallZ = 13.0;
		var wallSize = 7.0;
		var pixelSize = wallSize / canvasPixels;
		var sphere = new Sphere
		{
			Material = Material.Default() with
			{
				Color = new Color(1, 0.2, 1)
			}
		};

		var lightPosition = new Point(-10, 10, -10);
		var lightColor = new Color(1, 1, 1);
		var light = new PointLight(lightPosition, lightColor);

		var translation = -(wallSize * 0.5);
		var xRange = Enumerable.Range(0, (int)canvasPixels);
		var placeholderObj = new Sphere();
		Parallel.ForEach(xRange, x =>
		{
			for (var y = 0; y < canvasPixels; y++)
			{
				var directionVector = new Vector(x * pixelSize + translation, y * pixelSize + translation, wallZ).Normalize();
				var ray = new Ray(cameraPos, directionVector);
				var maybeHit = sphere.Intersect(ray).Hit();

				if (maybeHit.HasValue)
				{
					var hit = maybeHit.Value;
					var point = ray.Position(hit.T);
					var normal = sphere.NormalAt(point);
					var eye = -ray.Direction;
					var color = sphere.Material.Lighting(placeholderObj, light, point, eye, normal, false);
					canvas[x, (int)canvasPixels - y] = color;
				}
			}
		});

		return canvas;
	}

}