namespace RayTracerChallenge
{
	public static class PublicExtensions
	{
		public static bool AboutEqual(this double lhs, double rhs)
		{
			var aboutEqual = Math.Abs(lhs - rhs) < Constants.Epsilon;
			return aboutEqual;
		}

		/// <summary>
		/// Splits a string based on newlines, removing empty entries.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string[] SplitByLines(this string str)
		{
			return str.Split(["\r\n", "\n"], StringSplitOptions.RemoveEmptyEntries);
		}

		private const double ToRadiansConversion = Math.PI / 180.0;
		public static double ToRadians(this double degrees)
		{
			return ToRadiansConversion * degrees;
		}
	}
}
