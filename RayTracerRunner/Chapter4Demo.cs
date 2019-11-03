using System;
using System.IO;
using System.Linq;
using RayTracerChallenge;
using Tuple = RayTracerChallenge.Tuple;

namespace RayTracerRunner
{
    public static class Chapter4Demo
    {
        private static readonly int width = 850;
        private static readonly int height = 850;

        private static void DrawPixel(Canvas canvas, FColor pixelColor, Tuple point)
        {
            var translateToCanvasCoordinates = Transformation.Translation(width * 0.5, height * 0.5, 0);
            var translatedPt = translateToCanvasCoordinates * point;
            var xRounded = (int)Math.Ceiling(translatedPt.X);
            var yRounded = (int)Math.Ceiling(height - translatedPt.Y);
            foreach (var x in Enumerable.Range(xRounded - 3, 7))
            {
                foreach (var y in Enumerable.Range(yRounded - 3, 7))
                {
                    canvas.WritePixel(x, y, pixelColor);
                }
            }
        }

        public static void Run()
        {
            var canvas = new Canvas(width, height);

            var backgroundColor = new FColor(0, 0, 0);
            var pixelColor = new FColor(1, 1, 1);

            canvas.SetEveryPixel(backgroundColor);
            // 350 pixels straight up makes this hour 12
            var point = Tuple.Point(0, 350, 0);
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
