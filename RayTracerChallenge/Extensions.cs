namespace RayTracerChallenge
{
	internal static class Extensions
	{
		public static bool AboutEqual(this float lhs, float rhs)
		{
			const float tolerance = 0.000001f;
			var aboutEqual = Math.Abs(lhs - rhs) < tolerance;
			return aboutEqual;
		}

		private const float ToRadiansConversion = (float)Math.PI / 180f;
		public static float ToRadians(this float degrees)
		{
			return ToRadiansConversion * degrees;
		}
	}
}
