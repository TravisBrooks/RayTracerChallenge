using RayTracerChallenge;

namespace RayTracerTest
{
	public class CubeTest
	{
		[Theory]
		[MemberData(nameof(IntersectCubeData))]
		public void RayIntersectsCube(IntersectCubeDataType icParam)
		{
			var cube = new Cube();
			var ray = new Ray(icParam.Origin, icParam.Direction);
			var intersections = cube.Intersect(ray);
			Assert.Equal(2, intersections.Length);
			Assert.Equal(icParam.T1, intersections[0].T);
			Assert.Equal(icParam.T2, intersections[1].T);
		}

		[Theory]
		[MemberData(nameof(RayMissesCubeData))]
		public void RayMissesCube(Point origin, Vector direction)
		{
			var cube = new Cube();
			var ray = new Ray(origin, direction);
			var intersections = cube.Intersect(ray);
			Assert.Empty(intersections);
		}

		[Theory]
		[MemberData(nameof(NormalOnSurfaceOfCubeData))]
		public void NormalOnSurfaceOfCube(Point point, Vector expectedNormal)
		{
			var cube = new Cube();
			var normal = cube.NormalAt(point);
			Assert.Equal(expectedNormal, normal);
		}

		public record IntersectCubeDataType(string Comment, Point Origin, Vector Direction, double T1, double T2);

		public static IEnumerable<object[]> IntersectCubeData()
		{
			yield return [new IntersectCubeDataType("+x", new Point(5, 0.5, 0), new Vector(-1, 0, 0), 4, 6)];
			yield return [new IntersectCubeDataType("-x", new Point(-5, 0.5, 0), new Vector(1, 0, 0), 4, 6)];
			yield return [new IntersectCubeDataType("+y", new Point(0.5, 5.0, 0), new Vector(0, -1, 0), 4, 6)];
			yield return [new IntersectCubeDataType("-y", new Point(0.5, -5.0, 0), new Vector(0, 1, 0), 4, 6)];
			yield return [new IntersectCubeDataType("+z", new Point(0.5, 0, 5.0), new Vector(0, 0, -1), 4, 6)];
			yield return [new IntersectCubeDataType("-z", new Point(0.5, 0, -5.0), new Vector(0, 0, 1), 4, 6)];
			yield return [new IntersectCubeDataType("inside", new Point(0, 0.5, 0), new Vector(0, 0, 1), -1, 1)];
		}

		public static IEnumerable<object[]> RayMissesCubeData()
		{
			yield return [new Point(-2, 0, 0), new Vector(0.2673, 0.5345, 0.8018)];
			yield return [new Point(0, -2, 0), new Vector(0.8018, 0.2673, 0.5345)];
			yield return [new Point(0, 0, -2), new Vector(0.5345, 0.8018, 0.2673)];
			yield return [new Point(2, 0, 2), new Vector(0, 0, -1)];
			yield return [new Point(0, 2, 2), new Vector(0, -1, 0)];
			yield return [new Point(2, 2, 0), new Vector(-1, 0, 0)];
		}

		public static IEnumerable<object[]> NormalOnSurfaceOfCubeData()
		{
			yield return [new Point(1, 0.5, -0.8), new Vector(1, 0, 0)];
			yield return [new Point(-1, 0.2, 0.9), new Vector(-1, 0, 0)];
			yield return [new Point(-0.4, 1, -0.1), new Vector(0, 1, 0)];
			yield return [new Point(0.3, -1, -0.7), new Vector(0, -1, 0)];
			yield return [new Point(-0.6, 0.3, 1), new Vector(0, 0, 1)];
			yield return [new Point(0.4, 0.4, -1), new Vector(0, 0, -1)];
			yield return [new Point(1, 1, 1), new Vector(1, 0, 0)];
			yield return [new Point(-1, -1, -1), new Vector(-1, 0, 0)];
		}
	}
}