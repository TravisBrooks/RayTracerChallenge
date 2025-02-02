namespace RayTracerChallenge
{
	public readonly record struct Color(float Red, float Green, float Blue)
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

		public static Color operator *(Color c, float scalar)
		{
			var cScaled = new Color(c.Red * scalar, c.Green * scalar, c.Blue * scalar);
			return cScaled;
		}

		public static Color operator *(Color c1, Color c2)
		{
			var cProd = new Color(c1.Red * c2.Red, c1.Green * c2.Green, c1.Blue * c2.Blue);
			return cProd;
		}
	}
}
