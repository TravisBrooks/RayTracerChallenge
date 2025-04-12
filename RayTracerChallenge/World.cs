using System.Collections.Immutable;

namespace RayTracerChallenge
{
	public class World
	{
		public World()
		{
			Light = new PointLight(new Point(-10, 10, -10), new Color(1, 1, 1));
			var sphere1 = new Sphere
			{
				Material = Material.Default() with
				{
					Color = new Color(.8, 1, .6),
					Diffuse = .7,
					Specular = .2,

				}
			};
			var sphere2 = new Sphere
			{
				Transform = Transformation.Scaling(0.5, 0.5, 0.5)
			};
			Spheres.Add(sphere1);
			Spheres.Add(sphere2);
		}

		public World(PointLight light, IEnumerable<Sphere> spheres)
		{
			Light = light;
			Spheres = spheres.ToList();
		}

		public World(params Sphere[] spheres)
		{
			Light = new PointLight(new Point(-10, 10, -10), new Color(1, 1, 1));
			Spheres = spheres.ToList();
		}

		public IList<Sphere> Spheres { get; } = new List<Sphere>();
		public PointLight? Light { get; init; }

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
				comps.OverPoint,
				comps.EyeVector,
				comps.NormalVector,
				IsShadowed(comps.OverPoint)
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

		public bool IsShadowed(Point point)
		{
			if (!Light.HasValue)
			{
				return true;
			}
			var v = Light.Value.Position - point;
			var distance = v.Magnitude();
			var direction = v.Normalize();
			var ray = new Ray(point, direction);
			var intersections = Intersect(ray);
			var hit = intersections.Hit();
			return hit.HasValue && hit.Value.T < distance;
		}
	}
}
