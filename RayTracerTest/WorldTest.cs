using RayTracerChallenge;

namespace RayTracerTest
{
	public class WorldTest
	{
		[Fact]
		public void CreatingWorld()
		{
			var world = new World();
			Assert.Empty(world.Spheres);
			Assert.Null(world.Light);
		}

		[Fact]
		public void DefaultWorld()
		{
			var world = World.Default();
			var light = new PointLight(new Point(-10, 10, -10), new Color(1, 1, 1));
			var s1 = new Sphere();
			s1.Material = s1.Material with
			{
				Color = new Color(.8f, 1, .6f),
				Diffuse = .7f,
				Specular = .2f,
			};
			var s2 = new Sphere
			{
				Transform = Transformation.Scaling(0.5f, 0.5f, 0.5f)
			};
			Assert.Equal(light, world.Light);
			Assert.Equal(2, world.Spheres.Count);
			Assert.Contains(world.Spheres, s => s == s1);
			Assert.Contains(world.Spheres, s => s == s2);
		}

		[Fact]
		public void IntersectWorldWithRay()
		{
			var world = World.Default();
			var ray = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
			var intersections = world.Intersect(ray);
			Assert.Equal(4, intersections.Length);
			Assert.Equal(4f, intersections[0].T);
			Assert.Equal(4.5f, intersections[1].T);
			Assert.Equal(5.5f, intersections[2].T);
			Assert.Equal(6f, intersections[3].T);
		}

		[Fact]
		public void ShadingAnIntersection()
		{
			var w = World.Default();
			var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
			var shape = w.Spheres[0];
			var i = new Intersection(4f, shape);
			var comps = i.PrepareComputation(r);
			var c = w.ShadeHit(comps);
			Assert.Equal(new Color(0.38066f, 0.47583f, 0.2855f), c);
		}

		[Fact]
		public void ShadingAnIntersectionFromTheInside()
		{
			var w = World.Default();
			w.Light = new PointLight(new Point(0, .25f, 0), new Color(1, 1, 1));
			var r = new Ray(new Point(0, 0, 0), new Vector(0, 0, 1));
			var shape = w.Spheres[1];
			var i = new Intersection(0.5f, shape);
			var comps = i.PrepareComputation(r);
			var c = w.ShadeHit(comps);
			Assert.Equal(new Color(0.90498f, 0.90498f, 0.90498f), c);
		}

		[Fact]
		public void ColorAtWhenRayMisses()
		{
			var w = World.Default();
			var r = new Ray(new Point(0, 0, -5), new Vector(0, 1, 0));
			var c = w.ColorAt(r);
			Assert.Equal(new Color(0, 0, 0), c);
		}

		[Fact]
		public void ColorAtWhenRayHits()
		{
			var w = World.Default();
			var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
			var c = w.ColorAt(r);
			Assert.Equal(new Color(0.38066f, 0.47583f, 0.2855f), c);
		}

		[Fact]
		public void ColorAtWhenIntersectionBehindRay()
		{
			var w = World.Default();
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
			var r = new Ray(new Point(0, 0, 0.75f), new Vector(0, 0, -1));
			var c = w.ColorAt(r);
			// NOTE: the "* 0.1f" I added, the book errata lists a bug with this test because author was using
			// code that had a shadow calculation that's covered in a later chapter (test is from chapter 7) in
			// the book.
			Assert.Equal(inner.Material.Color * 0.1f, c);
		}
	}
}
