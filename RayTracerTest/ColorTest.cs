using RayTracerChallenge;

namespace RayTracerTest
{
	public class ColorTest
	{
		[Fact]
		public void PropertiesTest()
		{
			var c = new Color(-0.5, 0.4, 1.7);
			Assert.Equal(-0.5, c.Red);
			Assert.Equal(0.4, c.Green);
			Assert.Equal(1.7, c.Blue);
		}

		[Fact]
		public void AddColors()
		{
			var c1 = new Color(0.9, 0.6, 0.75);
			var c2 = new Color(0.7, 0.1, 0.25);
			var expected = new Color(1.6, 0.7, 1.0);
			var actual = c1 + c2;
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void SubtractColors()
		{
			var c1 = new Color(0.9, 0.6, 0.75);
			var c2 = new Color(0.7, 0.1, 0.25);
			var expected = new Color(0.2, 0.5, 0.5);
			var actual = c1 - c2;
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void MultiplyColorByScalar()
		{
			var c = new Color(0.2, 0.3, 0.4);
			var scalar = 2.0;
			var expected = new Color(0.4, 0.6, 0.8);
			var actual = c * scalar;
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void MultiplyColors()
		{
			var c1 = new Color(1, 0.2, 0.4);
			var c2 = new Color(0.9, 1, 0.1);
			var expected = new Color(0.9, 0.2, 0.04);
			var actual = c1 * c2;
			Assert.Equal(expected, actual);
		}
	}
}
