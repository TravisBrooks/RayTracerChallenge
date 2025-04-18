﻿using RayTracerChallenge.Patterns;

namespace RayTracerChallenge;

public readonly record struct Material(Color Color, double Ambient, double Diffuse, double Specular, double Shininess, BasePattern? Pattern)
{
	public static Material Default()
	{
		return new Material(
			Color: new Color(1, 1, 1),
			Ambient: 0.1,
			Diffuse: 0.9,
			Specular:0.9,
			Shininess: 200.0,
			Pattern: null);
	}

	public Color Lighting(BaseShape obj, PointLight light, Point point, Vector eyeVector, Vector normalVector, bool inShadow)
	{
		Color color;
		if (obj.Pattern is not null)
		{
			color = obj.Pattern.PatternAt(obj, point);
		}
		else if (Pattern is not null)
		{
			color = Pattern.PatternAt(obj, point);
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