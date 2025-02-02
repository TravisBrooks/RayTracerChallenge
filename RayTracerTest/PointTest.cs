using RayTracerChallenge;
using Point = RayTracerChallenge.Point;

namespace RayTracerTest
{
	public class PointTest
	{
		[Fact]
		public void PointPropertiesTest()
		{
			ITuple3D pt = new Point(4.3f, -4.2f, 3.1f);
			Assert.Equal(4.3f, pt.X);
			Assert.Equal(-4.2f, pt.Y);
			Assert.Equal(3.1f, pt.Z);
			Assert.Equal(1f, pt.W);
		}

		[Fact]
		public void PointAddVector()
		{
			var p = new Point(3, -2, 5);
			var v = new Vector(-2, 3, 1);
			var sum = p + v;
			Assert.Equal(1, sum.X);
			Assert.Equal(1, sum.Y);
			Assert.Equal(6, sum.Z);
			Assert.Equal(1, sum.W);
		}

		[Fact]
		public void PointMinusPoint()
		{
			var p1 = new Point(3, 2, 1);
			var p2 = new Point(5, 6, 7);
			var expected = new Vector(-2, -4, -6);
			var min = p1 - p2;
			Assert.Equal(expected, min);
		}

		[Fact]
		public void PointMinusVector()
		{
			var p = new Point(3, 2, 1);
			var v = new Vector(5, 6, 7);
			var expected = new Point(-2, -4, -6);
			var min = p - v;
			Assert.Equal(expected, min);
		}
	}
}
