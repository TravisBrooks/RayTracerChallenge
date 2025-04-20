namespace RayTracerChallenge;

public readonly record struct PhongComputation(
	double T,
	BaseShape Object,
	Point Point,
	Vector EyeVector,
	Vector NormalVector,
	bool Inside,
	Point OverPoint,
	Vector ReflectionVector,
	double N1,
	double N2,
	Point UnderPoint)
{
	/// <summary>
	/// The Schlick approximation for reflectance
	/// </summary>
	/// <returns></returns>
	public double Schlick()
	{
		var cos = EyeVector.DotProduct(NormalVector);
		if (N1 > N2)
		{
			var n = N1 / N2;
			var sin2T = Math.Pow(n, 2) * (1 - Math.Pow(cos, 2));
			if (sin2T > 1)
			{
				return 1;
			}

			cos = Math.Sqrt(1 - sin2T);
		}
		var r0 = Math.Pow((N1 - N2) / (N1 + N2), 2);
		return r0 + (1 - r0) * Math.Pow(1 - cos, 5);
	}
}