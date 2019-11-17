using System.IO;
using System.Linq;
using System.Threading.Tasks;
using RayTracerChallenge;

namespace RayTracerRunner
{
    public class Chapter5Demo : DemoRunner
    {
        protected override void RunImpl()
        {
            var canvasPixels = 500;
            var canvas = new Canvas(canvasPixels, canvasPixels);

            var backgroundColor = new FColor(0, 0, 0);
            var shadowColor = new FColor(1, 0, 0);
            var otherColor = new FColor(0, 0, 1);

            canvas.SetEveryPixel(backgroundColor);

            var cameraPos = Tuple3D.Point(0, 0, -5);
            var wallZ = 10.0;
            var wallSize = 7.0;
            var pixelSize = wallSize / canvasPixels;
            var sphere1 = new Sphere();
            var sphere2 = new Sphere
            {
                // shrink and skew
                Transform = Transformation.Scaling(0.5, 1, 1)
                                          .Shear(2, 0, 0, 0, 0, 0)
            };

            var xrange = Enumerable.Range(0, canvasPixels);
            Parallel.ForEach(xrange, x =>
            {
                for (var y = 0; y < canvasPixels; y++)
                {
                    var translation = -(wallSize * 0.5);
                    var directionVector = Tuple3D.Vector(x * pixelSize + translation, y * pixelSize + translation, wallZ);
                    var ray = new Ray(cameraPos, directionVector);
                    if (sphere1.Intersect(ray).Hit() != null)
                    {
                        canvas.WritePixel(x, y, canvas.PixelAt(x, y) + shadowColor);
                    }

                    if (sphere2.Intersect(ray).Hit() != null)
                    {
                        canvas.WritePixel(x, y, canvas.PixelAt(x, y) + otherColor);
                    }
                }
            });
            // 400x400, not parallel, for loops: Elapsed seconds: 11.3051724
            // 400x400, not parallel, foreach loops: Elapsed seconds: 11.2183499
            // 400x400, parallel, foreach x loop: Elapsed seconds: 3.4199165999999996
            // 400x400, parallel, foreach both loops: Elapsed seconds: 3.5285884
            // conclusion: making the xrange parallel worked fine, trying both in parallel was pointless.

            var ppm = canvas.ToPpm();
            File.WriteAllText("../../../Chapter5Demo.ppm", ppm);
        }
    }
}
