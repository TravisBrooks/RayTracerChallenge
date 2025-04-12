using RayTracerChallenge;

namespace RayTracerTest;

public class WorldTest
{
	[Fact]
	public void DefaultWorld()
	{
		var world = new World();
		var light = new PointLight(new Point(-10, 10, -10), new Color(1, 1, 1));
		var s1 = new Sphere
		{
			Material = Material.Default() with
			{
				Color = new Color(.8, 1, .6),
				Diffuse = .7,
				Specular = .2,
			}
		};
		var s2 = new Sphere
		{
			Transform = Transformation.Scaling(0.5, 0.5, 0.5)
		};
		Assert.Equal(light, world.Light);
		Assert.Equal(2, world.Spheres.Count);
		Assert.Contains(world.Spheres, s => s == s1);
		Assert.Contains(world.Spheres, s => s == s2);
	}

	[Fact]
	public void IntersectWorldWithRay()
	{
		var world = new World();
		var ray = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		var intersections = world.Intersect(ray);
		Assert.Equal(4.0, intersections.Length);
		Assert.Equal(4.0, intersections[0].T);
		Assert.Equal(4.5, intersections[1].T);
		Assert.Equal(5.5, intersections[2].T);
		Assert.Equal(6.0, intersections[3].T);
	}

	[Fact]
	public void ShadingAnIntersection()
	{
		var w = new World();
		var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		var shape = w.Spheres[0];
		var i = new Intersection(4.0, shape);
		var comps = i.PrepareComputation(r);
		var c = w.ShadeHit(comps);
		Assert.Equal(new Color(0.38066, 0.47583, 0.2855), c);
	}

	[Fact]
	public void ShadingAnIntersectionFromTheInside()
	{
		var w = new World
		{
			Light = new PointLight(new Point(0, .25, 0), new Color(1, 1, 1))
		};
		var r = new Ray(new Point(0, 0, 0), new Vector(0, 0, 1));
		var shape = w.Spheres[1];
		var i = new Intersection(0.5, shape);
		var comps = i.PrepareComputation(r);
		var c = w.ShadeHit(comps);
		Assert.Equal(new Color(0.90498, 0.90498, 0.90498), c);
	}

	[Fact]
	public void ColorAtWhenRayMisses()
	{
		var w = new World();
		var r = new Ray(new Point(0, 0, -5), new Vector(0, 1, 0));
		var c = w.ColorAt(r);
		Assert.Equal(new Color(0, 0, 0), c);
	}

	[Fact]
	public void ColorAtWhenRayHits()
	{
		var w = new World();
		var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		var c = w.ColorAt(r);
		Assert.Equal(new Color(0.38066, 0.47583, 0.2855), c);
	}

	[Fact]
	public void ColorAtWhenIntersectionBehindRay()
	{
		var w = new World();
		var outer = w.Spheres[0];
		outer.Material = outer.Material with
		{
			Ambient = 1
		};
		var inner = w.Spheres[1];
		inner.Material = inner.Material with
		{
			Ambient = 1
		};
		var r = new Ray(new Point(0, 0, 0.75), new Vector(0, 0, -1));
		var c = w.ColorAt(r);
		// NOTE: the "* 0.1" I added, the book errata lists a bug with this test because author was using
		// code that had a shadow calculation that's covered in a later chapter (test is from chapter 7) in
		// the book.
		Assert.Equal(inner.Material.Color * 0.1, c);
	}

	[Fact]
	public void NoShadowWhenNothingCollinearWithPointAndLight()
	{
		var w = new World();
		var p = new Point(0, 10, 0);
		Assert.False(w.IsShadowed(p));
	}

	[Fact]
	public void ShadowWhenObjectIsBetweenPointAndLight()
	{
		var w = new World();
		var p = new Point(10, -10, 10);
		Assert.True(w.IsShadowed(p));
	}

	[Fact]
	public void NoShadowWhenObjectIsBehindLight()
	{
		var w = new World();
		var p = new Point(-20, 20, -20);
		Assert.False(w.IsShadowed(p));
	}

	[Fact]
	public void NoShadowWhenObjectIsBehindPoint()
	{
		var w = new World();
		var p = new Point(-2, 2, -2);
		Assert.False(w.IsShadowed(p));
	}

	[Fact]
	public void ShadeHitGivenIntersectionInShadow()
	{
		var w = new World
		{
			Light = new PointLight(new Point(0, 0, -10), new Color(1, 1, 1))
		};
		var s1 = new Sphere();
		w.Spheres.Add(s1);
		var s2 = new Sphere
		{
			Transform = Transformation.Translation(0, 0, 10)
		};
		w.Spheres.Add(s2);
		var r = new Ray(new Point(0, 0, 5), new Vector(0, 0, 1));
		var i = new Intersection(4, s2);
		var comps = i.PrepareComputation(r);
		var c = w.ShadeHit(comps);
		Assert.Equal(new Color(0.1, 0.1, 0.1), c);
	}
}