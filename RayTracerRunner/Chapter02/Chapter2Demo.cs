using RayTracerChallenge;
using Environment = RayTracerRunner.Chapter02.Environment;

namespace RayTracerRunner.Chapter02;

public class Chapter2Demo : DemoRun
{
	protected override Canvas RunCanvasRender()
	{
		var velocity = new Vector(1, 1.8, 0).Normalize() * 11.25;
		var gravity = new Vector(0, -0.1, 0);
		var wind = new Vector(-0.01, 0, 0);
		var environment = new Environment(gravity, wind);
		var canvas = new Canvas(900, 550);
		canvas.SetAllPixels(new Color(1, 1, 1));

		var start = new Point(0, 0, 0);
		var projectileColor = Color.FromRgb(72, 164, 214);
		var projectile = new Projectile(start, velocity, projectileColor);

		while (projectile.Position.Y >= 0)
		{
			var x = (int)Math.Round(projectile.Position.X);
			// inverting the y coordinate to draw on canvas
			var y = canvas.Height - (int)Math.Round(projectile.Position.Y);
			if (x >= 0 && x < canvas.Width && y >= 0 && y < canvas.Height)
			{
				projectile.Draw(canvas);
			}
			projectile = Tick(environment, projectile);
		}
		return canvas;
	}

	private static Projectile Tick(Environment env, Projectile proj)
	{
		var pos = proj.Position + proj.Velocity;
		var vel = proj.Velocity + env.Gravity + env.Wind;
		var newProj = new Projectile(pos, vel, proj.Color);
		return newProj;
	}

}