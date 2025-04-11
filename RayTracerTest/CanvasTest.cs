using RayTracerChallenge;

namespace RayTracerTest
{
	public class CanvasTest
	{
		[Fact]
		public void CreatingCanvas()
		{
			var c = new Canvas(10, 20);
			Assert.Equal(10u, c.Width);
			Assert.Equal(20u, c.Height);
			var blackColor = new Color(0, 0, 0);
			for (var x = 0; x < c.Width; x++)
			{
				for (var y = 0; y < c.Height; y++)
				{
					Assert.Equal(blackColor, c[x, y]);
				}
			}
		}

		[Fact]
		public void WritingPixelToCanvas()
		{
			var c = new Canvas(10, 20);
			var red = new Color(1, 0, 0);
			c[2, 3] = red;
			Assert.Equal(red, c[2, 3]);
		}

		[Fact]
		public void ConstructingPpmHeader()
		{
			var c = new Canvas(5, 3);
			var ppmArr = c.ToPpm().SplitByLines();
			Assert.True(ppmArr.Length >= 3);
			Assert.Equal("P3", ppmArr[0]);
			Assert.Equal("5 3", ppmArr[1]);
			Assert.Equal("255", ppmArr[2]);
		}

		[Fact]
		public void ConstructingPpmPixelData()
		{
			var c = new Canvas(5, 3);
			var c1 = new Color(1.5f, 0, 0);
			var c2 = new Color(0, 0.5f, 0);
			var c3 = new Color(-0.5f, 0, 1);
			c[0, 0] = c1;
			c[2, 1] = c2;
			c[4, 2] = c3;
			var ppmArr = c.ToPpm().SplitByLines();
			Assert.Equal(6, ppmArr.Length);
			Assert.Equal("255 0 0 0 0 0 0 0 0 0 0 0 0 0 0", ppmArr[3]);
			Assert.Equal("0 0 0 0 0 0 0 128 0 0 0 0 0 0 0", ppmArr[4]);
			Assert.Equal("0 0 0 0 0 0 0 0 0 0 0 0 0 0 255", ppmArr[5]);
		}

		[Fact]
		public void PpmSplitsLongLines()
		{
			var c = new Canvas(10, 2);
			var color = new Color(1, 0.8f, 0.6f);
			for (var x = 0; x < c.Width; x++)
			{
				for (var y = 0; y < c.Height; y++)
				{
					c[x, y] = color;
				}
			}
			var ppmArr = c.ToPpm().SplitByLines();
			Assert.Equal(7, ppmArr.Length);
			Assert.Equal("255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204", ppmArr[3]);
			Assert.Equal("153 255 204 153 255 204 153 255 204 153 255 204 153", ppmArr[4]);
			Assert.Equal("255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204", ppmArr[5]);
			Assert.Equal("153 255 204 153 255 204 153 255 204 153 255 204 153", ppmArr[6]);
		}

		[Fact]
		public void PpmEndsWithNewline()
		{
			var c = new Canvas(5, 3);
			var ppm = c.ToPpm();
			Assert.EndsWith("\n", ppm);
		}
	}
}
