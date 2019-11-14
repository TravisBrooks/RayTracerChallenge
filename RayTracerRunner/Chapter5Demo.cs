using System.IO;
using RayTracerChallenge;

namespace RayTracerRunner
{
    public static class Chapter5Demo
    {
        public static void Run()
        {
            var canvasPixels = 400;
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
            var sphere2 = new Sphere();
            // shrink and skew
            sphere2.Transform = Transformation.Shearing(2, 0, 0, 0, 0, 0).Scale(0.5, 1, 1);

            for (var x=0; x < canvasPixels; x++) 
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
            }

            var ppm = canvas.ToPpm();
            File.WriteAllText("../../../Chapter5Demo.ppm", ppm);
        }
    }
}
