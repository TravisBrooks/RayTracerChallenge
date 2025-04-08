using RayTracerChallenge;

namespace RayTracerTest
{
	public class LightTest
	{
		[Fact]
		public void PointLightHasPositionAndIntensity()
		{
			var intensity = new Color(1, 1, 1);
			var position = new Point(0, 0, 0);
			var light = new PointLight(position, intensity);
			Assert.Equal(position, light.Position);
			Assert.Equal(intensity, light.Intensity);
		}
	}
}
