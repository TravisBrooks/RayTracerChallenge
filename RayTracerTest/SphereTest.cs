using RayTracerChallenge;

namespace RayTracerTest;

public class SphereTest
{
	[Fact]
	public void IntersectSetsTheObject()
	{
		var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		var s = new Sphere();
		var xs = s.Intersect(r);
		Assert.Equal(2, xs.Length);
		Assert.Equal(s, xs[0].Object);
		Assert.Equal(s, xs[1].Object);
	}

	[Fact]
	public void SphereHasDefaultTransform()
	{
		var s = new Sphere();
		Assert.Equal(Matrix.Identity(), s.Transform);
	}

	[Fact]
	public void ChangingSpheresTransform()
	{
		var s = new Sphere
		{
			Transform = Transformation.Translation(2, 3, 4)
		};
		Assert.Equal(Transformation.Translation(2, 3, 4), s.Transform);
	}

	[Fact]
	public void IntersectingScaledSphereWithRay()
	{
		var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		var s = new Sphere
		{
			Transform = Transformation.Scaling(2, 2, 2)
		};
		var xs = s.Intersect(r);
		Assert.Equal(2, xs.Length);
		Assert.Equal(3, xs[0].T);
		Assert.Equal(7, xs[1].T);
	}

	[Fact]
	public void IntersectingTranslatedSphereWithRay()
	{
		var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		var s = new Sphere
		{
			Transform = Transformation.Translation(5, 0, 0)
		};
		var xs = s.Intersect(r);
		Assert.Empty(xs);
	}

	[Fact]
	public void NormalOnTheX_Axis()
	{
		var s = new Sphere();
		var n = s.NormalAt(new Point(1, 0, 0));
		Assert.Equal(new Vector(1, 0, 0), n);
	}

	[Fact]
	public void NormalOnTheY_Axis()
	{
		var s = new Sphere();
		var n = s.NormalAt(new Point(0, 1, 0));
		Assert.Equal(new Vector(0, 1, 0), n);
	}

	[Fact]
	public void NormalOnTheZ_Axis()
	{
		var s = new Sphere();
		var n = s.NormalAt(new Point(0, 0, 1));
		Assert.Equal(new Vector(0, 0, 1), n);
	}

	[Fact]
	public void NormalOnNonAxialPoint()
	{
		var s = new Sphere();
		var sqrt3div3 = Math.Sqrt(3) / 3.0;
		var n = s.NormalAt(new Point(sqrt3div3, sqrt3div3, sqrt3div3));
		Assert.Equal(new Vector(sqrt3div3, sqrt3div3, sqrt3div3), n);
	}

	[Fact]
	public void NormalIsNormalizedVector()
	{
		var s = new Sphere();
		var sqrt3div3 = Math.Sqrt(3) / 3.0;
		var n = s.NormalAt(new Point(sqrt3div3, sqrt3div3, sqrt3div3));
		Assert.Equal(n.Normalize(), n);
	}

	[Fact]
	public void NormalOnTranslatedSphere()
	{
		var s = new Sphere
		{
			Transform = Transformation.Translation(0, 1, 0)
		};
		var n = s.NormalAt(new Point(0, 1.70711, -0.70711));
		Assert.Equal(new Vector(0, 0.7071, -0.70711), n);
	}

	[Fact]
	public void NormalOnTransformedSphere()
	{
		var s = new Sphere
		{
			Transform = Transformation.Scaling(1, 0.5, 1) * Transformation.RotationZ(Math.PI / 5.0)
		};
		var n = s.NormalAt(new Point(0, Math.Sqrt(2) / 2.0, -Math.Sqrt(2) / 2.0));
		Assert.Equal(new Vector(0, 0.97014, -0.24254), n);
	}

	[Fact]
	public void SphereHasDefaultMaterial()
	{
		var s = new Sphere();
		Assert.Equal(Material.Default(), s.Material);
	}

	[Fact]
	public void SphereCanBeAssignedMaterial()
	{
		var s = new Sphere
		{
			Material = Material.Default() with {Ambient = 1}
		};
		Assert.Equal(Material.Default() with {Ambient = 1}, s.Material);
	}
}