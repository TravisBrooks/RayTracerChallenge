using RayTracerChallenge;

namespace RayTracerRunner.Chapter08;

public class Chapter8Demo : DemoRun
{
	protected override Canvas RunCanvasRender()
	{
		var floor = new Sphere
		{
			Transform = Transformation.Scaling(10, 0.01, 10),
			Material = Material.Default() with
			{
				Color = new Color(1, 0.9, 0.9),
				Specular = 0,
			}
		};

		var leftWall = new Sphere
		{
			Transform = Transformation.Translation(0, 0, 5)
			            * Transformation.RotationY(-Math.PI / 4.0)
			            * Transformation.RotationX(Math.PI / 2.0)
			            * Transformation.Scaling(10, 0.01, 10),
			Material = floor.Material
		};

		var rightWall = new Sphere
		{
			Transform = Transformation.Translation(0, 0, 5)
			            * Transformation.RotationY(Math.PI / 4.0)
			            * Transformation.RotationX(Math.PI / 2.0)
			            * Transformation.Scaling(10, 0.01, 10),
			Material = floor.Material
		};

		var middle = new Sphere
		{
			Transform = Transformation.Translation(-0.5, 1, 0.5),
			Material = Material.Default() with
			{
				Color = new Color(0.1, 1, 0.5),
				Diffuse = 0.7,
				Specular = 0.3,
			}
		};

		var right = new Sphere
		{
			Transform = Transformation.Translation(1.5, 0.5, -0.5)
			            * Transformation.Scaling(0.5, 0.5, 0.5),
			Material = Material.Default() with
			{
				Color = new Color(0.5, .1, 1),
				Diffuse = 0.7,
				Specular = 0.3
			}
		};

		var left = new Sphere
		{
			Transform = Transformation.Translation(-1.5, 0.33, -0.75)
			            * Transformation.Scaling(0.33, 0.33, 0.33),
			Material = Material.Default() with
			{
				Color = new Color(1, 0.1, 0.1),
				Diffuse = 0.7,
				Specular = 0.3
			}
		};

		var world = new World([floor, leftWall, rightWall, middle, left, right], []);
		var camera = new Camera(1000, 500, Math.PI / 3.0)
		{
			Transform = Transformation.ViewTransform(new Point(0, 1.5, -5), new Point(0, 1, 0), new Vector(0, 1, 0))
		};
		var canvas = camera.Render(world);
		return canvas;
	}
}