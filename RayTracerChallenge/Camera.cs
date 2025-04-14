namespace RayTracerChallenge;

public class Camera
{
	public Camera(uint horizontalSize, uint verticalSize, double fieldOfView)
	{
		HorizontalSize = horizontalSize;
		VerticalSize = verticalSize;
		FieldOfView = fieldOfView;

		var halfView = Math.Tan(fieldOfView / 2.0);
		var aspect = horizontalSize / (double)verticalSize;
		if (aspect >= 1)
		{
			HalfWidth = halfView;
			HalfHeight = halfView / aspect;
		}
		else
		{
			HalfWidth = halfView * aspect;
			HalfHeight = halfView;
		}
		PixelSize = (HalfWidth * 2.0) / horizontalSize;
	}

	public uint HorizontalSize { get; }
	public uint VerticalSize { get; }
	public double FieldOfView { get; }
	public Matrix Transform { get; init; } = Matrix.Identity();
	public double PixelSize { get; }
	private double HalfWidth { get; }
	private double HalfHeight { get; }

	public Ray RayForPixel(int x, int y)
	{
		var xOffset = (x + 0.5) * PixelSize;
		var yOffset = (y + 0.5) * PixelSize;
		var worldX = HalfWidth - xOffset;
		var worldY = HalfHeight - yOffset;
		var inverse = Transform.Inverse();
		var pixel = (inverse * new Point(worldX, worldY, -1)).AssumePoint();
		var origin = (inverse * new Point(0, 0, 0)).AssumePoint();
		var direction = (pixel - origin).Normalize();
		return new Ray(origin, direction);
	}

	public Canvas Render(World world)
	{
		var canvas = new Canvas(HorizontalSize, VerticalSize);
		var tick = 0;
		var xRange = Enumerable.Range(0, (int)canvas.Width);
		Parallel.ForEach(
			xRange,
			new ParallelOptions
			{
				// Playing around with diff values, this seemed like a pretty good setting for my oldish laptop but
				// results will vary by hardware and how many other things you have running on your computer.
				MaxDegreeOfParallelism = (int)Math.Ceiling(Environment.ProcessorCount * 0.75 * 2.0)
			},
			x =>
			{
				for (var y = 0; y < canvas.Height; y++)
				{
					var ray = RayForPixel(x, y);
					var color = world.ColorAt(ray);
					canvas[x, y] = color;
					var currentTick = Interlocked.Increment(ref tick);
					if (currentTick % 1000 == 0)
					{
						Console.Write(".");
					}
				}
			}
		);
		Console.WriteLine();
		return canvas;
	}
}