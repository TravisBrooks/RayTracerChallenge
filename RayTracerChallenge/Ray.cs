namespace RayTracerChallenge;

public readonly record struct Ray(Point Origin, Vector Direction)
{
	public Point Position(double t)
	{
		var retVal = Origin + Direction * t;
		return retVal;
	}

	public Ray Transform(Matrix transform)
	{
		var newOrigin = transform * Origin;
		var newDirection = transform * Direction;
		var retVal = new Ray(newOrigin.AssumePoint(), newDirection.AssumeVector());
		return retVal;
	}
}