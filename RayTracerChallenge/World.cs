using System.Collections.Immutable;

namespace RayTracerChallenge
{
	public class World
	{
		public IList<Sphere> Spheres { get; } = new List<Sphere>();
		public PointLight? Light { get; set; }

		public static World Default()
		{
			var world = new World();
			var light = new PointLight(new Point(-10, 10, -10), new Color(1, 1, 1));
			world.Light = light;
			var sphere1 = new Sphere();
			sphere1.Material = sphere1.Material with
			{
				Color = new Color(.8f, 1, .6f),
				Diffuse = .7f,
				Specular = .2f,

			};
			var sphere2 = new Sphere
			{
				Transform = Transformation.Scaling(0.5f, 0.5f, 0.5f)
			};
			world.Spheres.Add(sphere1);
			world.Spheres.Add(sphere2);
			return world;
		}

		public ImmutableArray<Intersection> Intersect(Ray ray)
		{
			var intersections = new List<Intersection>();
			foreach (var s in Spheres)
			{
				intersections.AddRange(s.Intersect(ray));
			}

			return [..intersections.Distinct().OrderBy(i => i.T)];
		}

		public Color ShadeHit(PhongComputation comps)
		{
			if (!Light.HasValue)
			{
				return Color.FromRgb(0, 0, 0);
			}
			var c = comps.Object.Material.Lighting(
				Light.Value,
				comps.Point,
				comps.EyeVector,
				comps.NormalVector
			);
			return c;
		}

		public Color ColorAt(Ray ray)
		{
			var intersections = Intersect(ray);
			var hit = intersections.Hit();
			if (hit == null)
			{
				return Color.FromRgb(0, 0, 0);
			}
			var comps = hit.Value.PrepareComputation(ray);
			return ShadeHit(comps);
		}
	}
}
