namespace RayTracerChallenge;

public readonly record struct Color(double Red, double Green, double Blue)
{
	public bool Equals(Color other)
	{
		return Red.AboutEqual(other.Red) &&
		       Green.AboutEqual(other.Green) &&
		       Blue.AboutEqual(other.Blue);
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Red, Green, Blue);
	}

	public static Color operator +(Color c1, Color c2)
	{
		var cSum = new Color(c1.Red + c2.Red, c1.Green + c2.Green, c1.Blue + c2.Blue);
		return cSum;
	}

	public static Color operator -(Color c1, Color c2)
	{
		var cDiff = new Color(c1.Red - c2.Red, c1.Green - c2.Green, c1.Blue - c2.Blue);
		return cDiff;
	}

	public static Color operator *(Color c, double scalar)
	{
		var cScaled = new Color(c.Red * scalar, c.Green * scalar, c.Blue * scalar);
		return cScaled;
	}

	public static Color operator *(Color c1, Color c2)
	{
		var cProd = new Color(c1.Red * c2.Red, c1.Green * c2.Green, c1.Blue * c2.Blue);
		return cProd;
	}

	public static Color FromRgb(int r, int g, int b)
	{
		var c = new Color(r / 255.0, g / 255.0, b / 255.0);
		return c;
	}

	public (int red, int green, int blue) ToRgb()
	{
		var ppmColor = (red: _ToRgbComponent(Red), green: _ToRgbComponent(Green), blue: _ToRgbComponent(Blue));
		return ppmColor;
	}

	private static int _ToRgbComponent(double colorPart)
	{
		var ppmPart = (int)Math.Round(255 * colorPart);
		if (ppmPart < 0)
		{
			ppmPart = 0;
		}
		if (ppmPart > 255)
		{
			ppmPart = 255;
		}
		return ppmPart;
	}
}