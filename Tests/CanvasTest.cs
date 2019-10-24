using NUnit.Framework;
using RayTracerChallenge;

namespace Tests
{
    [TestFixture]
    public class CanvasTest
    {
        [Test]
        public void CreationTest()
        {
            var width = 10;
            var height = 20;
            var canvas = new Canvas(width, height);
            Assert.That(canvas.Width, Is.EqualTo(width), "width");
            Assert.That(canvas.Height, Is.EqualTo(height), "height");

            var black = new FColor(0, 0, 0);
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var color = canvas.PixelAt(x, y);
                    Assert.That(color, Is.EqualTo(black), "Unexpected color at ({0}, {1})", x, y);
                }
            }
        }

        [Test]
        public void WriteReadTest()
        {
            var canvas = new Canvas(10, 20);
            var red = new FColor(1, 0, 0);
            canvas.WritePixel(2, 3, red);
            var c = canvas.PixelAt(2, 3);
            Assert.That(c, Is.EqualTo(red));
        }

        [Test]
        public void PpmHeader()
        {
            var canvas = new Canvas(5, 3);
            var expected = @"P3
5 3
255";
            var ppm = canvas.ToPpm();
            var actualSlice = ppm.SliceLines(1, 3);

            // TODO: this test will pass on windows but fail if ran on other OS
            Assert.That(actualSlice, Is.EqualTo(expected));
        }

        [Test]
        public void PpmPixelData()
        {
            var canvas = new Canvas(5, 3);
            var c1 = new FColor(1.5, 0, 0);
            var c2 = new FColor(0, 0.5, 0);
            var c3 = new FColor(-0.5, 0, 1);
            canvas.WritePixel(0, 0, c1);
            canvas.WritePixel(2, 1, c2);
            canvas.WritePixel(4, 2, c3);

            var ppm = canvas.ToPpm();
            var actualSlice = ppm.SliceLines(4, 6);
            // NOTE: watch out copying/pasting this string from Kindle, when i tried that some goofy invisible unicode chars gets mixed in
            var expected = @"255 0 0 0 0 0 0 0 0 0 0 0 0 0 0
0 0 0 0 0 0 0 128 0 0 0 0 0 0 0
0 0 0 0 0 0 0 0 0 0 0 0 0 0 255";

            // TODO: this test will pass on windows but fail if ran on other OS
            Assert.That(actualSlice, Is.EqualTo(expected));
        }

        [Test]
        public void SplitLongPpmLines()
        {
            var canvas = new Canvas(10, 2);
            var color = new FColor(1, 0.8, 0.6);
            canvas.SetEveryPixel(color);

            var ppm = canvas.ToPpm();
            var actualSlice = ppm.SliceLines(4, 7);

            var expected = @"255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204
153 255 204 153 255 204 153 255 204 153 255 204 153
255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204
153 255 204 153 255 204 153 255 204 153 255 204 153";

            // TODO: this test will pass on windows but fail if ran on other OS
            Assert.That(actualSlice, Is.EqualTo(expected));
        }

        [Test]
        public void PpmTerminatedByNewLine()
        {
            var canvas = new Canvas(5, 3);
            var ppm = canvas.ToPpm();
            var expected = '\n';
            var actual = ppm[^1];
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
