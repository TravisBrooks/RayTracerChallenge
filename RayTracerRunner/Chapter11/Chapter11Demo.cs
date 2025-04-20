using RayTracerChallenge;
using RayTracerChallenge.Patterns;

namespace RayTracerRunner.Chapter11
{
	public class Chapter11Demo : DemoRun
	{
		protected override Canvas RunCanvasRender()
		{
			var floor = new Plane
			{
				Material = Material.Default() with
				{
					Color = new Color(1, 0.9, 0.9),
					Specular = 0,
					Reflectivity = 1.0,
					Ambient = 0.4
				},
				Pattern = new CheckersPattern(Color.White, Color.Black)
			};

			var middle = new Sphere
			{
				Transform = Transformation.Translation(-0.5, 1, 0.5),
				Material = Material.Default() with
				{
					Color = new Color(0.1, 1, 0.5),
					Diffuse = 0.2,
					Specular = 1,
					Reflectivity = 0.9,
					Transparency = 0.9,
					RefractiveIndex = 1.5,
					Shininess = 300
				}
			};

			var right = new Sphere
			{
				Transform = Transformation.Translation(1.5, 0.5, -0.5)
				            * Transformation.Scaling(0.5, 0.5, 0.5),
				Material = Material.Default() with
				{
					Color = new Color(0.5, .1, 1),
					Diffuse = 0.2,
					Specular = 1,
					Reflectivity = 0.9,
					Transparency = 0.9,
					RefractiveIndex = 1.5,
					Shininess = 300
				}
			};

			var left = new Sphere
			{
				Transform = Transformation.Translation(-1.5, 0.33, -0.75)
				            * Transformation.Scaling(0.33, 0.33, 0.33),
				Material = Material.Default() with
				{
					Color = Color.FromRgb(139, 0, 0),
					Diffuse = 0.2,
					Specular = 1,
					Reflectivity = 0.9,
					Transparency = 0.9,
					RefractiveIndex = 1.5,
					Shininess = 300
				}
			};

			var world = new World([middle, left, right], [floor]);
			var camera = new Camera(3000, 1500, Math.PI / 3.0)
			{
				Transform = Transformation.ViewTransform(new Point(7, 1.5, -5), new Point(0, 1, 0), new Vector(0, 1, 0))
			};
			var canvas = camera.Render(world);
			return canvas;
		}
	}
}
