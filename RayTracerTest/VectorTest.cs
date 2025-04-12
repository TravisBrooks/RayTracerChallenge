using RayTracerChallenge;

namespace RayTracerTest;

public class VectorTest
{
	[Fact]
	public void VectorPropertiesTest()
	{
		ITuple3D v = new Vector(4.3, -4.2, 3.1);
		Assert.Equal(4.3, v.X);
		Assert.Equal(-4.2, v.Y);
		Assert.Equal(3.1, v.Z);
		Assert.Equal(0, v.W);
	}

	[Fact]
	public void VectorAddPoint()
	{
		var v = new Vector(-2, 3, 1);
		var p = new Point(3, -2, 5);
		var sum = v + p;
		Assert.Equal(1, sum.X);
		Assert.Equal(1, sum.Y);
		Assert.Equal(6, sum.Z);
		Assert.Equal(1, sum.W);
	}

	[Fact]
	public void VectorAddVector()
	{
		var v1 = new Vector(-2, 3, 1);
		var v2 = new Vector(3, -2, 5);
		var sum = v1 + v2;
		Assert.Equal(1, sum.X);
		Assert.Equal(1, sum.Y);
		Assert.Equal(6, sum.Z);
		Assert.Equal(0, sum.W);
	}

	[Fact]
	public void VectorMinusVector()
	{
		var v1 = new Vector(3, 2, 1);
		var v2 = new Vector(5, 6, 7);
		var expected = new Vector(-2, -4, -6);
		var min = v1 - v2;
		Assert.Equal(expected, min);
	}

	[Fact]
	public void VectorNegation()
	{
		var v = new Vector(1, 2, 3);
		var expected = new Vector(-1, -2, -3);
		var actual = -v;
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void VectorScalarMultiplication()
	{
		var v = new Vector(1, -2, 3);
		var scalar = .5;
		var expected = new Vector(.5, -1, 1.5);
		var actual = v * scalar;
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void VectorScalarDivision()
	{
		var v = new Vector(1, -2, 3);
		var scalar = 2.0;
		var expected = new Vector(.5, -1, 1.5);
		var actual = v / scalar;
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void MagnitudeX()
	{
		var v = new Vector(1, 0, 0);
		var expected = 1.0;
		var actual = v.Magnitude();
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void MagnitudeY()
	{
		var v = new Vector(0, 1, 0);
		var expected = 1.0;
		var actual = v.Magnitude();
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void MagnitudeZ()
	{
		var v = new Vector(0, 0, 1);
		var expected = 1.0;
		var actual = v.Magnitude();
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void MagnitudeComplexPositive()
	{
		var v = new Vector(1, 2, 3);
		var expected = Math.Sqrt(14);
		var actual = v.Magnitude();
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void MagnitudeComplexNegative()
	{
		var v = new Vector(-1, -2, -3);
		var expected = Math.Sqrt(14);
		var actual = v.Magnitude();
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void VectorNormalizeBasic()
	{
		var v = new Vector(4, 0, 0);
		var expected = new Vector(1, 0, 0);
		var actual = v.Normalize();
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void VectorNormalizeFancy()
	{
		var v = new Vector(1, 2, 3);
		// vector(1/√14,   2/√14,   3/√14)​
		var expected = new Vector(0.26726124, 0.5345225, 0.8017837);
		var actual = v.Normalize();
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void MagnitudeOfNormalizedVector()
	{
		var v = new Vector(1, 2, 3);
		var norm = v.Normalize();
		var expected = 1.0;
		var actual = norm.Magnitude();
		Assert.Equal(expected, actual, precision:6);
	}

	[Fact]
	public void DotProduct()
	{
		var v1 = new Vector(1, 2, 3);
		var v2 = new Vector(2, 3, 4);
		var expected = 20.0;
		var actual = v1.DotProduct(v2);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void CrossProductAtoB()
	{
		var v1 = new Vector(1, 2, 3);
		var v2 = new Vector(2, 3, 4);
		var expected = new Vector(-1, 2, -1);
		var actual = v1.CrossProduct(v2);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void CrossProductBtoA()
	{
		var v1 = new Vector(1, 2, 3);
		var v2 = new Vector(2, 3, 4);
		var expected = new Vector(1, -2, 1);
		var actual = v2.CrossProduct(v1);
		Assert.Equal(expected, actual);
	}

}