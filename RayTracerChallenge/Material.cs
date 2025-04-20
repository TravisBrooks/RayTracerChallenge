using RayTracerChallenge.Patterns;

namespace RayTracerChallenge;

public readonly record struct Material(
	Color Color,
	double Ambient = 0.1,
	double Diffuse = 0.9,
	double Specular = 0.9,
	double Shininess = 200.0,
	BasePattern? Pattern = null,
	double Reflectivity = 0.0,
	double Transparency = 0.0,
	double RefractiveIndex = 1.0)
{
	public static Material Default()
	{
		return new Material(Color: new Color(1, 1, 1));
	}

	public Color Lighting(
		BaseShape shape,
		PointLight light,
		Point point,
		Vector eyeVector,
		Vector normalVector,
		bool inShadow)
	{
		Color color;
		if (shape.Pattern is not null)
		{
			color = shape.Pattern.PatternAt(shape, point);
		}
		else if (Pattern is not null)
		{
			color = Pattern.PatternAt(shape, point);
		}
		else
		{
			color = Color;
		}
		var effectiveColor = color * light.Intensity;
		var lightVector = (light.Position - point).Normalize();
		var ambient = effectiveColor * Ambient;
			
		if (inShadow)
		{
			return ambient;
		}

		var lightDotNormal = lightVector.DotProduct(normalVector);
		Color diffuse;
		Color specular;

		if (lightDotNormal < 0)
		{
			// Light is on the other side of the surface
			diffuse = new Color(0, 0, 0);
			specular = new Color(0, 0, 0);
		}
		else
		{
			// Diffuse component
			diffuse = effectiveColor * Diffuse * lightDotNormal;

			// Specular component
			var reflectVector = (-lightVector).Reflect(normalVector); // Correct reflection direction
			var reflectDotEye = reflectVector.DotProduct(eyeVector);

			if (reflectDotEye <= 0)
			{
				// Reflection is away from the eye
				specular = new Color(0, 0, 0);
			}
			else
			{
				// Reflection is visible
				var factor = Math.Pow(reflectDotEye, Shininess);
				specular = light.Intensity * Specular * factor;
			}
		}

		return ambient + diffuse + specular;
	}
}