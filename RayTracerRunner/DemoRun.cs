using System.Diagnostics;
using RayTracerChallenge;

namespace RayTracerRunner
{
	public abstract class DemoRun
	{
		protected abstract Canvas RunCanvasRender();

		public void Run()
		{
			// run and time the render
			var stopwatch = new Stopwatch();
			stopwatch.Start();
			var canvas = RunCanvasRender();
			stopwatch.Stop();
			Console.WriteLine($"{GetType().Name} {nameof(RunCanvasRender)} elapsed seconds: {stopwatch.Elapsed.TotalSeconds}");

			// run and time the ppm file from the rendered canvas
			stopwatch = new Stopwatch();
			stopwatch.Start();
			var ppmString = BuildPpm(canvas);
			stopwatch.Stop();
			Console.WriteLine($"{GetType().Name} {nameof(BuildPpm)} elapsed seconds: {stopwatch.Elapsed.TotalSeconds}");

			// write and time the ppm file disk I/O
			stopwatch = new Stopwatch();
			stopwatch.Start();
			WritePpm(ppmString, GetType().Name + ".ppm");
			stopwatch.Stop();
			Console.WriteLine($"{GetType().Name} {nameof(WritePpm)} elapsed seconds: {stopwatch.Elapsed.TotalSeconds}");
		}

		private void WritePpm(string ppmString, string ppmFileName)
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
			File.WriteAllText(Path.Combine(finalDestination.FullName, ppmFileName), ppmString);
		}

		private string BuildPpm(Canvas canvas)
		{
			var ppm = canvas.ToPpm();
			return ppm;
		}
	}
}
