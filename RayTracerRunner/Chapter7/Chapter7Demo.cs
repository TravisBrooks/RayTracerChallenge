using RayTracerChallenge;

namespace RayTracerRunner.Chapter7
{
	public class Chapter7Demo : DemoRun
	{
		protected override Canvas RunCanvasRender()
		{
			var floor = new Sphere
			{
				Transform = Transformation.Scaling(10, 0.01f, 10),
			};
			floor.Material = floor.Material with
			{
				Color = new Color(1, 0.9f, 0.9f),
				Specular = 0,
			};

			var leftWall = new Sphere
			{
				Transform = Transformation.Translation(0, 0, 5) 
				            * Transformation.RotationY(-MathF.PI / 4) 
				            * Transformation.RotationX(MathF.PI / 2) 
				            * Transformation.Scaling(10, 0.01f, 10),
				Material = floor.Material
			};

			var rightWall = new Sphere
			{
				Transform = Transformation.Translation(0, 0, 5)
							* Transformation.RotationY(MathF.PI / 4)
							* Transformation.RotationX(MathF.PI / 2)
							* Transformation.Scaling(10, 0.01f, 10),
				Material = floor.Material
			};

			var middle = new Sphere
			{
				Transform = Transformation.Translation(-0.5f, 1, 0.5f),
				Material = Material.Default() with
				{
					Color = new Color(0.1f, 1, 0.5f),
					Diffuse = 0.7f,
					Specular = 0.3f,
				}
			};

			var right = new Sphere
			{
				Transform = Transformation.Translation(1.5f, 0.5f, -0.5f)
							* Transformation.Scaling(0.5f, 0.5f, 0.5f),
				Material = Material.Default() with
				{
					Color = new Color(0.5f, .1f, 1),
					Diffuse = 0.7f,
					Specular = 0.3f,
				}
			};

			var left = new Sphere
			{
				Transform = Transformation.Translation(-1.5f, 0.33f, -0.75f)
							* Transformation.Scaling(0.33f, 0.33f, 0.33f),
				Material = Material.Default() with
				{
					Color = new Color(1, 0.1f, 0.1f),
					Diffuse = 0.7f,
					Specular = 0.3f,
				}
			};

			var world = new World
			{
				Spheres = { floor, leftWall, rightWall, middle, left, right },
				Light = new PointLight(new Point(-10, 10, -10), new Color(1, 1, 1))
			};

			var camera = new Camera(1000, 500, MathF.PI / 3)
			{
				Transform = Transformation.ViewTransform(new Point(0, 1.5f, -5), new Point(0, 1, 0), new Vector(0, 1, 0))
			};
			var canvas = camera.Render(world);
			return canvas;
		}
	}
}
