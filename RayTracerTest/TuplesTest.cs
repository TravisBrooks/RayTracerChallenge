using RayTracerChallenge;

namespace RayTracerTest;

public class TuplesTest
{
	[Fact]
	public void ReflectingVectorApproaching45Degrees()
	{
		var v = new Vector(1, -1, 0);
		var n = new Vector(0, 1, 0);
		var r = v.Reflect(n);
		Assert.Equal(new Vector(1, 1, 0), r);
	}

	[Fact]
	public void ReflectingVectorOffSlantedSurface()
	{
		var v = new Vector(0, -1, 0);
		var n = new Vector(Math.Sqrt(2) / 2.0, Math.Sqrt(2) / 2.0, 0);
		var r = v.Reflect(n);
		Assert.Equal(new Vector(1, 0, 0), r);
	}

}