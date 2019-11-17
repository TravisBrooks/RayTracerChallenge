using System;
using System.IO;
using System.Linq;
using RayTracerChallenge;

namespace RayTracerRunner
{
    public class Chapter4Demo : DemoRunner
    {
        private static readonly int width = 850;
        private static readonly int height = 850;

        private static void DrawPixel(Canvas canvas, FColor pixelColor, Tuple3D point)
        {
            // the screen coordinates are the lower right of the logical coordinate so need to shift to right and invert x
            var translateToCanvasCoordinates = Transformation.Translation(width * 0.5, height * 0.5, 0)
                                                             .RotateX(Math.PI);
            var translatedPt = translateToCanvasCoordinates * point;
            var xRounded = (int)Math.Ceiling(translatedPt.X);
            var yRounded = (int)Math.Ceiling(translatedPt.Y);
            foreach (var x in Enumerable.Range(xRounded - 3, 7))
            {
                foreach (var y in Enumerable.Range(yRounded - 3, 7))
                {
                    canvas.WritePixel(x, y, pixelColor);
                }
            }
        }

        protected override void RunImpl()
        {
            var canvas = new Canvas(width, height);

            var backgroundColor = new FColor(0, 0, 0);
            var pixelColor = new FColor(1, 1, 1);

            canvas.SetEveryPixel(backgroundColor);
            // 350 pixels straight up makes this hour 12
            var point = Tuple3D.Point(0, 350, 0);
            DrawPixel(canvas, pixelColor, point);

            // this draws the other points in reverse order, so hour 11, 10, ...
            var transform = Transformation.RotationZ(Math.PI / 6);
            Enumerable.Range(2, 11).ToList().ForEach(_ =>
            {
                point = transform * point;
                DrawPixel(canvas, pixelColor, point);
            });

            var ppm = canvas.ToPpm();
            File.WriteAllText("../../../Chapter4Demo.ppm", ppm);
        }
    }
}
