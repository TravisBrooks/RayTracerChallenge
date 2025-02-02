using RayTracerChallenge;

namespace RayTracerTest
{
	public class ColorTest
	{
		[Fact]
		public void PropertiesTest()
		{
			var c = new Color(-0.5f, 0.4f, 1.7f);
			Assert.Equal(-0.5f, c.Red);
			Assert.Equal(0.4f, c.Green);
			Assert.Equal(1.7f, c.Blue);
		}

		[Fact]
		public void AddColors()
		{
			var c1 = new Color(0.9f, 0.6f, 0.75f);
			var c2 = new Color(0.7f, 0.1f, 0.25f);
			var expected = new Color(1.6f, 0.7f, 1.0f);
			var actual = c1 + c2;
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void SubtractColors()
		{
			var c1 = new Color(0.9f, 0.6f, 0.75f);
			var c2 = new Color(0.7f, 0.1f, 0.25f);
			var expected = new Color(0.2f, 0.5f, 0.5f);
			var actual = c1 - c2;
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void MultiplyColorByScalar()
		{
			var c = new Color(0.2f, 0.3f, 0.4f);
			var scalar = 2f;
			var expected = new Color(0.4f, 0.6f, 0.8f);
			var actual = c * scalar;
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void MultiplyColors()
		{
			var c1 = new Color(1, 0.2f, 0.4f);
			var c2 = new Color(0.9f, 1, 0.1f);
			var expected = new Color(0.9f, 0.2f, 0.04f);
			var actual = c1 * c2;
			Assert.Equal(expected, actual);
		}
	}
}
