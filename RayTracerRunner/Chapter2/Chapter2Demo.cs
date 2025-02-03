using RayTracerChallenge;

namespace RayTracerRunner.Chapter2
{
	public class Chapter2Demo : DemoRun
	{
		protected override Canvas RunCanvasRender()
		{
			var velocity = new Vector(1, 1.8f, 0).Normalize() * 11.25f;
			var gravity = new Vector(0, -0.1f, 0);
			var wind = new Vector(-0.01f, 0, 0);
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

		protected override string BuildPpm(Canvas canvas)
		{
			var ppm = canvas.ToPpm();
			return ppm;
		}

		protected override void WritePpm(string ppmString)
		{
			var projectDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
			while (!string.Equals(projectDir?.Name, "bin", StringComparison.OrdinalIgnoreCase))
			{
				projectDir = projectDir?.Parent;
			}
			var finalDestination = projectDir?.Parent;
			if (finalDestination is null)
			{
				throw new DirectoryNotFoundException("Could not find the project directory");
			}

			File.WriteAllText(Path.Combine(finalDestination.FullName, "Chapter2Demo.ppm"), ppmString);
		}

	}
}
