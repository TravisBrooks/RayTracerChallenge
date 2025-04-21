using RayTracerChallenge;
using Point = RayTracerChallenge.Point;

namespace RayTracerRunner.Chapter12
{
	public class Chapter12Demo : DemoRun
	{
		protected override Canvas RunCanvasRender()
		{
			var tableTop = new Cube
			{
				Transform = Transformation.Scaling(2.5, .1, 1.5),
				Material = Material.Default() with
				{
					Ambient = .3,
					Color = new Color(1, 0.8, 0.6)
				}
			};
			var tableLegLeftFront = new Cube
			{
				Transform = Transformation.Translation(-2.4, -2, -1.35) * Transformation.Scaling(.1, 2, .1),
				Material = Material.Default() with
				{
					Ambient = .3,
					Color = new Color(1, 0.4, 0.4)
				}
			};
			var tableLegLeftRear = new Cube
			{
				Transform = Transformation.Translation(-2.4, -2, 1.35) * Transformation.Scaling(.1, 2, .1),
				Material = Material.Default() with
				{
					Ambient = .3,
					Color = new Color(1, 0.4, 0.4)
				}
			};
			var tableLegRightFront = new Cube
			{
				Transform = Transformation.Translation(2.4, -2, -1.35) * Transformation.Scaling(.1, 2, .1),
				Material = Material.Default() with
				{
					Ambient = .3,
					Color = new Color(1, 0.4, 0.4)
				}
			};
			var tableLegRightRear = new Cube
			{
				Transform = Transformation.Translation(2.4, -2, 1.35) * Transformation.Scaling(.1, 2, .1),
				Material = Material.Default() with
				{
					Ambient = .3,
					Color = new Color(1, 0.4, 0.4)
				}
			};
			var floor = new Cube
			{
				Transform = Transformation.Translation(0.1, -2.5, .01) 
				            * Transformation.Scaling(8, .1, 8),
				Material = Material.Default() with
				{
					Color = Color.FromRgb(255, 222, 173)
				}
			};
			var sphere = new Sphere
			{
				Transform = Transformation.Translation(0, 1.1, 0),
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
			var world = new World([sphere], [], [floor, tableTop, tableLegLeftFront, tableLegLeftRear, tableLegRightFront, tableLegRightRear]);
			var camera = new Camera(2496, 1664, Math.PI / 3.0)
			{
				Transform = Transformation.ViewTransform(new Point(7, 1.5, -17), new Point(0, 1, 0), new Vector(0, 1, 0))
			};
			var canvas = camera.Render(world);
			return canvas;
		}
	}
}
