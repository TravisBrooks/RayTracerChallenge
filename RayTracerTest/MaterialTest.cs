using RayTracerChallenge;

namespace RayTracerTest
{
	public class MaterialTest
	{
		[Fact]
		public void DefaultMaterial()
		{
			var m = Material.Default();
			Assert.Equal(new Color(1, 1, 1), m.Color);
			Assert.Equal(0.1f, m.Ambient);
			Assert.Equal(0.9f, m.Diffuse);
			Assert.Equal(0.9f, m.Specular);
			Assert.Equal(200.0f, m.Shininess);
		}

		[Fact]
		public void LightingWithEyeBetweenLightAndSurface()
		{
			var m = Material.Default();
			var position = new Point(0, 0, 0);
			var eye = new Vector(0, 0, -1);
			var normal = new Vector(0, 0, -1);
			var light = new PointLight(new Point(0, 0, -10), new Color(1, 1, 1));
			var result = m.Lighting(light, position, eye, normal);
			Assert.Equal(new Color(1.9f, 1.9f, 1.9f), result);
		}

		[Fact]
		public void LightingWithEyeBetweenLightAndSurfaceEyeOffset45Degrees()
		{
			var m = Material.Default();
			var position = new Point(0, 0, 0);
			var eye = new Vector(0, MathF.Sqrt(2) / 2f, -MathF.Sqrt(2) / 2f);
			var normal = new Vector(0, 0, -1);
			var light = new PointLight(new Point(0, 0, -10), new Color(1, 1, 1));
			var result = m.Lighting(light, position, eye, normal);
			Assert.Equal(new Color(1f, 1f, 1f), result);
		}

		[Fact]
		public void LightWithEyeOppositeSurfaceLightOffset45Degrees()
		{
			var m = Material.Default();
			var position = new Point(0, 0, 0);
			var eye = new Vector(0, 0, -1);
			var normal = new Vector(0, 0, -1);
			var light = new PointLight(new Point(0, 10, -10), new Color(1, 1, 1));
			var result = m.Lighting(light, position, eye, normal);
			Assert.Equal(new Color(.7364f, .7364f, .7364f), result);
		}

		[Fact]
		public void LightingWithEyeInPathOfReflectionVector()
		{
			var m = Material.Default();
			var position = new Point(0, 0, 0);
			var eye = new Vector(0, -MathF.Sqrt(2) / 2f, -MathF.Sqrt(2) / 2f);
			var normal = new Vector(0, 0, -1);
			var light = new PointLight(new Point(0, 10, -10), new Color(1, 1, 1));
			var result = m.Lighting(light, position, eye, normal);
			Assert.Equal(new Color(1.63638f, 1.63638f, 1.63638f), result);
		}

		[Fact]
		public void LightWithLightBehindTheSurface()
		{
			var m = Material.Default();
			var position = new Point(0, 0, 0);
			var eye = new Vector(0, 0, -1);
			var normal = new Vector(0, 0, -1);
			var light = new PointLight(new Point(0, 0, 10), new Color(1, 1, 1));
			var result = m.Lighting(light, position, eye, normal);
			Assert.Equal(new Color(0.1f, 0.1f, 0.1f), result);
		}
	}
}
