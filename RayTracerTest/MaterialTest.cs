using RayTracerChallenge;
using RayTracerChallenge.Patterns;

namespace RayTracerTest;

public class MaterialTest
{
	[Fact]
	public void DefaultMaterial()
	{
		var m = Material.Default();
		Assert.Equal(new Color(1, 1, 1), m.Color);
		Assert.Equal(0.1, m.Ambient);
		Assert.Equal(0.9, m.Diffuse);
		Assert.Equal(0.9, m.Specular);
		Assert.Equal(200.0, m.Shininess);
	}

	[Fact]
	public void LightingWithEyeBetweenLightAndSurface()
	{
		var m = Material.Default();
		var position = new Point(0, 0, 0);
		var eye = new Vector(0, 0, -1);
		var normal = new Vector(0, 0, -1);
		var light = new PointLight(new Point(0, 0, -10), new Color(1, 1, 1));
		var result = m.Lighting(new TestShape(), light, position, eye, normal, false);
		Assert.Equal(new Color(1.9, 1.9, 1.9), result);
	}

	[Fact]
	public void LightingWithEyeBetweenLightAndSurfaceEyeOffset45Degrees()
	{
		var m = Material.Default();
		var position = new Point(0, 0, 0);
		var eye = new Vector(0, Math.Sqrt(2) / 2.0, -Math.Sqrt(2) / 2.0);
		var normal = new Vector(0, 0, -1);
		var light = new PointLight(new Point(0, 0, -10), new Color(1, 1, 1));
		var result = m.Lighting(new TestShape(), light, position, eye, normal, false);
		Assert.Equal(new Color(1, 1, 1), result);
	}

	[Fact]
	public void LightWithEyeOppositeSurfaceLightOffset45Degrees()
	{
		var m = Material.Default();
		var position = new Point(0, 0, 0);
		var eye = new Vector(0, 0, -1);
		var normal = new Vector(0, 0, -1);
		var light = new PointLight(new Point(0, 10, -10), new Color(1, 1, 1));
		var result = m.Lighting(new TestShape(), light, position, eye, normal, false);
		Assert.Equal(new Color(.7364, .7364, .7364), result);
	}

	[Fact]
	public void LightingWithEyeInPathOfReflectionVector()
	{
		var m = Material.Default();
		var position = new Point(0, 0, 0);
		var eye = new Vector(0, -Math.Sqrt(2) / 2.0, -Math.Sqrt(2) / 2.0);
		var normal = new Vector(0, 0, -1);
		var light = new PointLight(new Point(0, 10, -10), new Color(1, 1, 1));
		var result = m.Lighting(new TestShape(), light, position, eye, normal, false);
		Assert.Equal(new Color(1.63638, 1.63638, 1.63638), result);
	}

	[Fact]
	public void LightWithLightBehindTheSurface()
	{
		var m = Material.Default();
		var position = new Point(0, 0, 0);
		var eye = new Vector(0, 0, -1);
		var normal = new Vector(0, 0, -1);
		var light = new PointLight(new Point(0, 0, 10), new Color(1, 1, 1));
		var result = m.Lighting(new TestShape(), light, position, eye, normal, false);
		Assert.Equal(new Color(0.1, 0.1, 0.1), result);
	}

	[Fact]
	public void LightingWithTheSurfaceInShadow()
	{
		var m = Material.Default();
		var position = new Point(0, 0, 0);
		var eye = new Vector(0, 0, -1);
		var normal = new Vector(0, 0, -1);
		var light = new PointLight(new Point(0, 0, -10), new Color(1, 1, 1));
		var inShadow = true;
		var result = m.Lighting(new TestShape(), light, position, eye, normal, inShadow);
		Assert.Equal(new Color(.1, .1, .1), result);
	}

	[Fact]
	public void LightingWithPatternApplied()
	{
		var m = Material.Default() with
		{
			Ambient = 1,
			Diffuse = 0,
			Specular = 0,
			Pattern = new StripePattern(Color.White, Color.Black)
		};
		var eye = new Vector(0, 0, -1);
		var normal = new Vector(0, 0, -1);
		var light = new PointLight(new Point(0, 0, -10), new Color(1, 1, 1));
		var c1 = m.Lighting(new TestShape(), light, new Point(0.9, 0, 0), eye, normal, false);
		var c2 = m.Lighting(new TestShape(), light, new Point(1.1, 0, 0), eye, normal, false);
		Assert.Equal(Color.White, c1);
		Assert.Equal(Color.Black, c2);
	}

	[Fact]
	public void ReflectivityForDefaultMaterial()
	{
		var m = Material.Default();
		Assert.Equal(0.0, m.Reflectivity);
	}

	[Fact]
	public void TransparencyAndRefractiveIndexForDefaultMaterial()
	{
		var m = Material.Default();
		Assert.Equal(0.0, m.Transparency);
		Assert.Equal(1.0, m.RefractiveIndex);
	}
}