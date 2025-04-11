namespace RayTracerChallenge
{
	public class Camera
	{
		public Camera(uint horizontalSize, uint verticalSize, float fieldOfView)
		{
			HorizontalSize = horizontalSize;
			VerticalSize = verticalSize;
			FieldOfView = fieldOfView;

			var halfView = MathF.Tan(fieldOfView / 2f);
			var aspect = horizontalSize / ((float)verticalSize);
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
			PixelSize = (HalfWidth * 2f) / horizontalSize;
		}

		public uint HorizontalSize { get; }
		public uint VerticalSize { get; }
		public float FieldOfView { get; }
		public Matrix Transform { get; init; } = Matrix.Identity();
		public float PixelSize { get; }
		private float HalfWidth { get; }
		private float HalfHeight { get; }

		public Ray RayForPixel(int x, int y)
		{
			var xOffset = (x + 0.5f) * PixelSize;
			var yOffset = (y + 0.5f) * PixelSize;
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
			for (var x = 0; x < canvas.Width; x++)
			{
				for (var y = 0; y<canvas.Height; y++)
				{
					var ray = RayForPixel(x, y);
					var color = world.ColorAt(ray);
					canvas[x, y] = color;
					tick++;
					if (tick % 1000 == 0)
					{
						Console.Write(".");
					}
				}
			}
			Console.WriteLine();
			return canvas;
		}
		
	}
}
