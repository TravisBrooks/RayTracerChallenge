using System.Diagnostics;
using RayTracerChallenge;

namespace RayTracerRunner
{
	public abstract class DemoRun
	{
		protected abstract Canvas RunCanvasRender();
		protected abstract void WritePpm(Canvas canvas);

		public void Run()
		{
			var stopwatch = new Stopwatch();
			stopwatch.Start();
			var canvas = RunCanvasRender();
			stopwatch.Stop();

			Console.WriteLine($"{GetType().Name} {nameof(RunCanvasRender)} Elapsed seconds: {stopwatch.Elapsed.TotalSeconds}");
			WritePpm(canvas);
		}
	}
}
