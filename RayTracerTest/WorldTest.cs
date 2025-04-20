using System.Diagnostics;
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
		// doing a bunch of stuff on the default world so World can have init set properties
		var defaultWorld = new World();
		Debug.Assert(defaultWorld.Light is not null);
		var light = defaultWorld.Light.Value;
		var outer = new Sphere()
		{
			Transform = defaultWorld.Spheres[0].Transform,
			Material = defaultWorld.Spheres[0].Material with
			{
				Ambient = 1
			}
		};
		var inner = new Sphere()
		{
			Transform = defaultWorld.Spheres[1].Transform,
			Material = defaultWorld.Spheres[1].Material with
			{
				Ambient = 1
			}
		};

		var w = new World(light, [outer, inner]);
		var r = new Ray(new Point(0, 0, 0.75), new Vector(0, 0, -1));
		var c = w.ColorAt(r);

		Assert.Equal(inner.Material.Color, c);
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

	[Fact]
	public void ReflectedColorForNonReflectiveMaterial()
	{
		var shape = new Sphere
		{
			Transform = Transformation.Scaling(0.5, 0.5, 0.5),
			Material = Material.Default() with
			{
				Ambient = 1
			}
		};
		var w = new World([shape], []);
		var r = new Ray(new Point(0, 0, 0), new Vector(0, 0, 1));
		var i = new Intersection(1, shape);
		var comps = i.PrepareComputation(r);
		var color = w.ReflectedColor(comps);
		Assert.Equal(new Color(0, 0, 0), color);
	}

	[Fact]
	public void ReflectedColorForReflectiveMaterial()
	{
		var shape = new Plane
		{
			Transform = Transformation.Translation(0, -1, 0),
			Material = Material.Default() with
			{
				Reflectivity = 0.5
			}
		};
		var w = new World(new World().Spheres, [shape]);
		var r = new Ray(new Point(0, 0, -3), new Vector(0, -Math.Sqrt(2)/2.0, Math.Sqrt(2) / 2.0));
		var i = new Intersection(Math.Sqrt(2), shape);
		var comps = i.PrepareComputation(r);
		var color = w.ReflectedColor(comps);
		Assert.Equal(new Color(0.19032, 0.2379, 0.14274), color);
	}

	[Fact]
	public void ShadeHitWithReflectiveMaterial()
	{
		var shape = new Plane
		{
			Transform = Transformation.Translation(0, -1, 0),
			Material = Material.Default() with
			{
				Reflectivity = 0.5
			}
		};
		var w = new World(new World().Spheres, [shape]);
		var r = new Ray(new Point(0, 0, -3), new Vector(0, -Math.Sqrt(2) / 2.0, Math.Sqrt(2) / 2.0));
		var i = new Intersection(Math.Sqrt(2), shape);
		var comps = i.PrepareComputation(r);
		var color = w.ShadeHit(comps);
		Assert.Equal(new Color(0.87677, 0.92436, 0.82918), color);
	}

	[Fact]
	public void ColorAtWithMutuallyReflectiveSurfaces()
	{
		var light= new PointLight(new Point(0, 0, 0), new Color(1, 1, 1));
		var lower = new Plane
		{
			Transform = Transformation.Translation(0, -1, 0),
			Material = Material.Default() with
			{
				Reflectivity = 1
			}
		};
		var upper = new Plane
		{
			Transform = Transformation.Translation(0, 1, 0),
			Material = Material.Default() with
			{
				Reflectivity = 1
			}
		};
		var w = new World(light, [lower, upper]);
		var r = new Ray(new Point(0, 0, 0), new Vector(0, 1, 0));
		// if infinite recursion happens this throws a StackOverflowException, which cannot be caught by C#...
		w.ColorAt(r);
	}

	[Fact]
	public void ReflectedColorAtMaximumRecursiveDepth()
	{
		var shape = new Plane
		{
			Transform = Transformation.Translation(0, -1, 0),
			Material = Material.Default() with
			{
				Reflectivity = 0.5
			}
		};
		var w = new World(new World().Spheres, [shape]);
		var r = new Ray(new Point(0, 0, -3), new Vector(0, -Math.Sqrt(2) / 2.0, Math.Sqrt(2) / 2.0));
		var i = new Intersection(Math.Sqrt(2), shape);
		var comps = i.PrepareComputation(r);
		var color = w.ReflectedColor(comps, 0);
		Assert.Equal(Color.Black, color);
	}

	[Fact]
	public void RefractedColorWithOpaqueSurface()
	{
		var w = new World();
		var shape = w.Spheres[0];
		var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		var i1 = new Intersection(4, shape);
		var i2 = new Intersection(6, shape);
		var comps = i1.PrepareComputation(r, Intersection.Aggregate(i1, i2));
		var color = w.RefractedColor(comps, 5);
		Assert.Equal(Color.Black, color);
	}

	[Fact]
	public void RefractedColorAtMaximumRecursiveDepth()
	{
		var sphere = new Sphere
		{
			Material = Material.Default() with
			{
				Transparency = 1,
				RefractiveIndex = 1.5
			}
		};
		var w = new World([sphere], new List<Plane>());
		var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		var i1 = new Intersection(4, sphere);
		var i2 = new Intersection(6, sphere);
		var comps = i1.PrepareComputation(r, Intersection.Aggregate(i1, i2));
		var color = w.RefractedColor(comps, 0);
		Assert.Equal(Color.Black, color);
	}

	[Fact]
	public void RefractedColorWithTotalInternalReflection()
	{
		var sphere = new Sphere
		{
			Material = Material.Default() with
			{
				Transparency = 1,
				RefractiveIndex = 1.5
			}
		};
		var w = new World([sphere], new List<Plane>());
		var r = new Ray(new Point(0, 0, Math.Sqrt(2)/2), new Vector(0, 1, 0));
		var i1 = new Intersection(-Math.Sqrt(2)/2.0, sphere);
		var i2 = new Intersection(Math.Sqrt(2)/2.0, sphere);
		var comps = i2.PrepareComputation(r, Intersection.Aggregate(i1, i2));
		var color = w.RefractedColor(comps, 5);
		Assert.Equal(Color.Black, color);
	}

	[Fact]
	public void RefractedColorWithRefractedRay()
	{
		var sphere1 = new Sphere
		{
			Material = Material.Default() with
			{
				Ambient = 1,
				Pattern = new TestPattern(),
				Color = new Color(.8, 1, .6),
				Diffuse = .7,
				Specular = .2,
			}
		};
		var sphere2 = new Sphere
		{
			Transform = Transformation.Scaling(0.5, 0.5, 0.5),
			Material = Material.Default() with
			{
				Transparency = 1,
				RefractiveIndex = 1.5
			}
		};
		var w = new World([sphere1, sphere2], new List<Plane>());
		var r = new Ray(new Point(0, 0, 0.1), new Vector(0, 1, 0));
		var i1 = new Intersection(-0.9899, sphere1);
		var i2 = new Intersection(-0.4899, sphere2);
		var i3 = new Intersection(0.4899, sphere2);
		var i4 = new Intersection(0.9899, sphere1);
		var comps = i3.PrepareComputation(r, Intersection.Aggregate(i1, i2, i3, i4));
		var color = w.RefractedColor(comps, 5);
		Assert.Equal(new Color(0, 0.99888, 0.04725), color);
	}

	[Fact]
	public void ShadeHitWithTransparentMaterial()
	{
		var w = new World();
		var floor = new Plane
		{
			Transform = Transformation.Translation(0, -1, 0),
			Material = Material.Default() with
			{
				Transparency = 0.5,
				RefractiveIndex = 1.5
			}
		};
		var ball = new Sphere
		{
			Transform = Transformation.Translation(0, -3.5, -0.5),
			Material = Material.Default() with
			{
				Color = new Color(1, 0, 0),
				Ambient = 0.5
			}
		};
		w.Planes.Add(floor);
		w.Spheres.Add(ball);
		var r = new Ray(new Point(0, 0, -3), new Vector(0, -Math.Sqrt(2)/2.0, Math.Sqrt(2) / 2.0));
		var i = new Intersection(Math.Sqrt(2), floor);
		var xs = Intersection.Aggregate(i);
		var comps = i.PrepareComputation(r, xs);
		var c = w.ShadeHit(comps, 5);
		Assert.Equal(new Color(0.93642, 0.68642, 0.68642), c);
	}

	[Fact]
	public void ShadeHitWithReflectiveTransparentMaterial()
	{
		var w = new World();
		var floor = new Plane
		{
			Transform = Transformation.Translation(0, -1, 0),
			Material = Material.Default() with
			{
				Reflectivity = 0.5,
				Transparency = 0.5,
				RefractiveIndex = 1.5
			}
		};
		var ball = new Sphere
		{
			Transform = Transformation.Translation(0, -3.5, -0.5),
			Material = Material.Default() with
			{
				Color = new Color(1, 0, 0),
				Ambient = 0.5
			}
		};
		w.Planes.Add(floor);
		w.Spheres.Add(ball);
		var r = new Ray(new Point(0, 0, -3), new Vector(0, -Math.Sqrt(2) / 2.0, Math.Sqrt(2) / 2.0));
		var i = new Intersection(Math.Sqrt(2), floor);
		var xs = Intersection.Aggregate(i);
		var comps = i.PrepareComputation(r, xs);
		var c = w.ShadeHit(comps, 5);
		Assert.Equal(new Color(0.93391, 0.69643, 0.69243), c);
	}
}