using RayTracerChallenge;

namespace RayTracerRunner.Chapter6
{
	internal class Chapter6Demo : DemoRun
	{
		protected override Canvas RunCanvasRender()
		{
			var canvasPixels = 500;
			var canvas = new Canvas(canvasPixels, canvasPixels);

			var backgroundColor = Color.FromRgb(0, 0, 0);
			canvas.SetAllPixels(backgroundColor);

			var cameraPos = new Point(0, 0, -5);
			var wallZ = 13.0f;
			var wallSize = 7.0f;
			var pixelSize = wallSize / canvasPixels;
			IIntersectable sphere = new Sphere
			{
				Material = Material.Default() with
				{
					Color = new Color(1, 0.2f, 1)
				}
			};

			var lightPosition = new Point(-10, 10, -10);
			var lightColor = new Color(1, 1, 1);
			var light = new PointLight(lightPosition, lightColor);

			var translation = -(wallSize * 0.5f);
			var xRange = Enumerable.Range(0, canvasPixels);
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
						var obj = hit.Object;
						var normal = obj.NormalAt(point);
						var eye = -ray.Direction;
						var color = obj.Material.Lighting(light, point, eye, normal);
						canvas[x, canvasPixels - y] = color;
					}
				}
			});

			return canvas;
		}

	}
}
