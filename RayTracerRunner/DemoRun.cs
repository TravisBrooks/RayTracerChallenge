using System.Diagnostics;
using RayTracerChallenge;

namespace RayTracerRunner
{
	public abstract class DemoRun
	{
		protected abstract Canvas RunCanvasRender();
		protected abstract string BuildPpm(Canvas canvas);
		protected abstract void WritePpm(string ppmString);

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
			WritePpm(ppmString);
			stopwatch.Stop();
			Console.WriteLine($"{GetType().Name} {nameof(WritePpm)} elapsed seconds: {stopwatch.Elapsed.TotalSeconds}");
		}
	}
}
