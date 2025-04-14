using RayTracerChallenge;

namespace RayTracerTest;

public class ShapeTest
{
	[Fact]
	public void TheDefaultTransformation()
	{
		var testShape = new TestShape();
		Assert.Equal(Matrix.Identity(), testShape.Transform);
	}

	[Fact]
	public void AssigningTransformation()
	{
		var testShape = new TestShape
		{
			Transform = Transformation.Translation(2, 3, 4)
		};
		Assert.Equal(Transformation.Translation(2, 3, 4), testShape.Transform);
	}

	[Fact]
	public void TheDefaultMaterial()
	{
		var testShape = new TestShape();
		Assert.Equal(Material.Default(), testShape.Material);
	}

	[Fact]
	public void AssigningMaterial()
	{
		var m = new Material(){
			Ambient = 1
		};
		var testShape = new TestShape
		{
			Material = m
		};
		Assert.Equal(m, testShape.Material);
	}

	[Fact]
	public void IntersectingScaledShapeWithRay()
	{
		var r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		var s = new TestShape
		{
			Transform = Transformation.Scaling(2, 2, 2)
		};
		var xs = s.Intersect(r);
		Assert.Equal(new Point(0, 0, -2.5), s.RayInTransformedSpace.Origin);
		Assert.Equal(new Vector(0, 0, .5), s.RayInTransformedSpace.Direction);
	}

	[Fact]
	public void ComputingTheNormalOnTranslatedShape()
	{
		var s = new TestShape
		{
			Transform = Transformation.Translation(0, 1, 0)
		};
		var n = s.NormalAt(new Point(0, 1.70711, -0.70711));
		Assert.Equal(new Vector(0, 0.70711, -0.70711), n);
	}

	[Fact]
	public void ComputingNormalOnTransformedShape()
	{
		var s = new TestShape
		{
			Transform = Transformation.Scaling(1, 0.5, 1) * Transformation.RotationZ(Math.PI / 5)
		};
		var n = s.NormalAt(new Point(0, Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2));
		Assert.Equal(new Vector(0, 0.97014, -0.24254), n);
	}
}