using RayTracerChallenge;

namespace RayTracerTest;

public class CameraTest
{
	[Fact]
	public void ConstructingCamera()
	{
		var hSize = 160u;
		var vSize = 120u;
		var fov = Math.PI / 2;
		var transform = Matrix.Identity();
		var camera = new Camera(hSize, vSize, fov);
		Assert.Equal(hSize, camera.HorizontalSize);
		Assert.Equal(vSize, camera.VerticalSize);
		Assert.Equal(fov, camera.FieldOfView);
		Assert.Equal(transform, camera.Transform);
	}

	[Fact]
	public void PixelSizeForHorizontalCanvas()
	{
		var camera = new Camera(200, 125, Math.PI / 2);
		var pixelSize = camera.PixelSize;
		Assert.True(pixelSize.AboutEqual(0.01));
	}

	[Fact]
	public void PixelSizeForVerticalCanvas()
	{
		var camera = new Camera(125, 200, Math.PI / 2);
		var pixelSize = camera.PixelSize;
		Assert.True(pixelSize.AboutEqual(0.01));
	}

	[Fact]
	public void RayForPixelThroughCenterOfCanvas()
	{
		var camera = new Camera(201, 101, Math.PI / 2);
		var ray = camera.RayForPixel(100, 50);
		Assert.Equal(new Point(0, 0, 0), ray.Origin);
		Assert.Equal(new Vector(0, 0, -1), ray.Direction);
	}

	[Fact]
	public void RayForPixelThroughCornerOfCanvas()
	{
		var camera = new Camera(201, 101, Math.PI / 2);
		var ray = camera.RayForPixel(0, 0);
		Assert.Equal(new Point(0, 0, 0), ray.Origin);
		Assert.Equal(new Vector(0.66519, 0.33259, -0.66851), ray.Direction);
	}

	[Fact]
	public void RayForPixelWhenCameraTransformed()
	{
		var camera = new Camera(201, 101, Math.PI / 2)
		{
			Transform = Transformation.RotationY(Math.PI / 4) * Transformation.Translation(0, -2, 5)
		};
		var ray = camera.RayForPixel(100, 50);
		Assert.Equal(new Point(0, 2, -5), ray.Origin);
		Assert.Equal(new Vector(Math.Sqrt(2) / 2, 0, -Math.Sqrt(2) / 2), ray.Direction);
	}

	[Fact]
	public void RenderingWorldWithCamera()
	{
		var world = new World();
		var from = new Point(0, 0, -5);
		var to = new Point(0, 0, 0);
		var up = new Vector(0, 1, 0);
		var camera = new Camera(11, 11, Math.PI / 2)
		{
			Transform = Transformation.ViewTransform(from, to, up)
		};
		var canvas = camera.Render(world);
		Assert.Equal(new Color(0.38066, 0.4758, 0.2855), canvas[5, 5]);
	}	
}