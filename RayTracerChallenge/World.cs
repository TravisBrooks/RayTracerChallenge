using System.Collections.Immutable;

namespace RayTracerChallenge;

public class World
{
	private const int MaxRecursionDepth = 4;

	public World(
		PointLight light,
		IEnumerable<Sphere> spheres,
		IEnumerable<Plane> planes,
		IEnumerable<Cube> cubes)
	{
		Light = light;
		Spheres = spheres.ToList();
		Planes = planes.ToList();
		Cubes = cubes.ToList();
	}

	public World(IEnumerable<Sphere> spheres, IEnumerable<Plane> planes, IEnumerable<Cube> cubes)
		: this(new PointLight(new Point(-10, 10, -10), new Color(1, 1, 1)), spheres, planes, cubes)
	{
	}

	public World() : this(
		new List<Sphere>
		{ 
			new()
			{
				Material = Material.Default() with
				{
					Color = new Color(.8, 1, .6),
					Diffuse = .7,
					Specular = .2,
				}
			}, 
			new()
			{
				Transform = Transformation.Scaling(0.5, 0.5, 0.5)
			}
		},
		new List<Plane>(),
		new List<Cube>())
	{
	}

	public World(PointLight light, IEnumerable<Sphere> spheres) 
		: this(light, spheres, new List<Plane>(), new List<Cube>())
	{
	}

	public World(PointLight light, IEnumerable<Plane> planes) 
		: this(light, new List<Sphere>(), planes, new List<Cube>())
	{
	}

	public PointLight? Light { get; init; }
	public IList<Sphere> Spheres { get; init; }
	public IList<Plane> Planes { get; init; }
	public IList<Cube> Cubes { get; init; }
	
	public IList<BaseShape> AllShapes
	{
		get
		{
			var shapes = new List<BaseShape>();
			shapes.AddRange(Spheres);
			shapes.AddRange(Planes);
			shapes.AddRange(Cubes);
			return shapes;
		}
	}

	public ImmutableArray<Intersection> Intersect(Ray ray)
	{
		var intersections = new List<Intersection>();
		foreach (var s in AllShapes)
		{
			intersections.AddRange(s.Intersect(ray));
		}

		return [..intersections.Distinct().OrderBy(i => i.T)];
	}

	public Color ShadeHit(PhongComputation comps, int remaining = MaxRecursionDepth)
	{
		if (!Light.HasValue)
		{
			return Color.FromRgb(0, 0, 0);
		}
		var surface = comps.Object.Material.Lighting(
			comps.Object,
			Light.Value,
			comps.OverPoint,
			comps.EyeVector,
			comps.NormalVector,
			IsShadowed(comps.OverPoint)
		);
		var reflected = ReflectedColor(comps, remaining);
		var refracted = RefractedColor(comps, remaining);
		var material = comps.Object.Material;
		if (material is { Reflectivity: > 0, Transparency: > 0 })
		{
			var reflectance = comps.Schlick();
			return surface + reflected * reflectance + refracted * (1 - reflectance);
		}
		return surface + reflected + refracted;
	}

	public Color ColorAt(Ray ray, int remaining = MaxRecursionDepth)
	{
		var intersections = Intersect(ray);
		var hit = intersections.Hit();
		if (hit == null)
		{
			return Color.FromRgb(0, 0, 0);
		}
		var comps = hit.Value.PrepareComputation(ray, intersections);
		return ShadeHit(comps, remaining);
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

	public Color ReflectedColor(PhongComputation comps, int remaining = MaxRecursionDepth)
	{
		if (comps.Object.Material.Reflectivity == 0 || remaining<=0 )
		{
			return Color.Black;
		}
		var reflectionRay = new Ray(comps.OverPoint, comps.ReflectionVector);
		var color = ColorAt(reflectionRay, remaining - 1);
		return color * comps.Object.Material.Reflectivity;
	}

	public Color RefractedColor(PhongComputation comps, int remaining = MaxRecursionDepth)
	{
		if (comps.Object.Material.Transparency == 0 || remaining <= 0)
		{
			return Color.Black;
		}
		var nRatio = comps.N1 / comps.N2;
		var cosI = comps.EyeVector.DotProduct(comps.NormalVector);
		var sin2T = nRatio * nRatio * (1 - cosI * cosI);
		if (sin2T > 1) // total internal reflection
		{
			return Color.Black;
		}
		var cosT = Math.Sqrt(1 - sin2T);
		var direction = comps.NormalVector * (nRatio * cosI - cosT) - comps.EyeVector * nRatio;
		var refractedRay = new Ray(comps.UnderPoint, direction);
		return ColorAt(refractedRay, remaining - 1) * comps.Object.Material.Transparency;
	}
}