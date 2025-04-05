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
			var baseDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
			while (!string.Equals(baseDir?.Name, "bin", StringComparison.OrdinalIgnoreCase))
			{
				baseDir = baseDir?.Parent;
			}

			var projectDir = baseDir?.Parent;
			if (projectDir is null)
			{
				throw new DirectoryNotFoundException("Could not find the project directory");
			}

			// a pretty hacky way to move the ppm files into the chapter directories
			var instanceNamespace = GetType().Namespace!.Split('.');
			var topLevelNamespace = typeof(DemoRun).Namespace;
			var toChapterNamespace = string.Join(Path.DirectorySeparatorChar, instanceNamespace.SkipWhile(s => s.Equals(topLevelNamespace)));
			var chapterPath = Path.Combine(projectDir.FullName, toChapterNamespace);
			File.WriteAllText(Path.Combine(chapterPath, ppmFileName), ppmString);
		}

		private string BuildPpm(Canvas canvas)
		{
			var ppm = canvas.ToPpm();
			return ppm;
		}
	}
}
