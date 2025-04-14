namespace RayTracerChallenge;

public record struct PhongComputation(
	double T,
	BaseShape Object,
	Point Point,
	Vector EyeVector,
	Vector NormalVector,
	bool Inside,
	Point OverPoint);