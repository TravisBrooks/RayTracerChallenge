namespace RayTracerChallenge
{
	public record struct PhongComputation(
		float T,
		IIntersectable Object,
		Point Point,
		Vector EyeVector,
		Vector NormalVector,
		bool Inside)
	{
	}
}
