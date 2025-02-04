namespace RayTracerChallenge
{
	internal static class InternalExtensions
	{
		public static bool AboutEqual(this float lhs, float rhs)
		{
			const float tolerance = 0.00001f;
			var aboutEqual = Math.Abs(lhs - rhs) < tolerance;
			return aboutEqual;
		}

		private const float ToRadiansConversion = (float)Math.PI / 180f;
		public static float ToRadians(this float degrees)
		{
			return ToRadiansConversion * degrees;
		}
	}

	public static class PublicExtensions
	{
		/// <summary>
		/// Splits a string based on newlines, removing empty entries.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string[] SplitByLines(this string str)
		{
			return str.Split(["\r\n", "\n"], StringSplitOptions.RemoveEmptyEntries);
		}
	}
}
