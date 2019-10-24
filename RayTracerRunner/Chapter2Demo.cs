using System;
using System.IO;
using RayTracerChallenge;
using Tuple = RayTracerChallenge.Tuple;

namespace RayTracerRunner
{
    public static class Chapter2Demo
    {

        public static Projectile Tick(Environment env, Projectile proj)
        {
            var pos = proj.Position + proj.Velocity;
            var vel = proj.Velocity + env.Gravity + env.Wind;
            var newProj = new Projectile(pos, vel);
            return newProj;
        }

        public static void Run()
        {
            var position = Tuple.Point(0, 1, 0);
            var velocity = Tuple.Vector(1, 1.8, 0).Normalize() * 11.25;
            var proj = new Projectile(position, velocity);
            var gravity = Tuple.Vector(0, -0.1, 0);
            var wind = Tuple.Vector(-0.01, 0, 0);
            var env = new Environment(gravity, wind);

            var width = 900;
            var height = 550;
            var canvas = new Canvas(width, height);

            //var skyBlue = new FColor(0.529, 0.808, 0.922);
            var supermanBlue = FColor.FromRGB(72, 164, 214);
            var supermanRed = FColor.FromRGB(200, 0, 0);
            var projectileColor = supermanRed;
            var backgroundColor = supermanBlue;
            var blendColor = projectileColor * backgroundColor;
            canvas.SetEveryPixel(backgroundColor);

            var keepGoing = true;
            while (keepGoing)
            {
                var xRounded = (int)Math.Ceiling(proj.Position.X);
                var yRounded = (int)Math.Ceiling(height - proj.Position.Y);
                if (xRounded >= 0 && yRounded >= 0 && xRounded < width && yRounded < height)
                {
                    canvas.WritePixel(xRounded, yRounded, projectileColor);

                    // writing more pixels to copy the "exaggerated" pixels from book.
                    // solid color for the "+" portions of pixel
                    canvas.WritePixel(xRounded, Math.Min(yRounded+1, height-1), projectileColor);
                    canvas.WritePixel(xRounded, Math.Max(yRounded-1, 0), projectileColor);
                    canvas.WritePixel(Math.Min(xRounded+1, width-1), yRounded, projectileColor);
                    canvas.WritePixel(Math.Max(xRounded-1, 0), yRounded, projectileColor);
                    // blended color for corner "x" portions of pixel
                    canvas.WritePixel(Math.Min(xRounded + 1, width - 1), Math.Min(yRounded + 1, height - 1), blendColor);
                    canvas.WritePixel(Math.Min(xRounded + 1, width - 1), Math.Max(yRounded - 1, 0), blendColor);
                    canvas.WritePixel(Math.Max(xRounded - 1, 0), Math.Max(yRounded - 1, 0), blendColor);
                    canvas.WritePixel(Math.Max(xRounded - 1, 0), Math.Min(yRounded + 1, height -1), blendColor);
                }

                keepGoing = xRounded >= 0;
                proj = Tick(env, proj);
            }
            var ppm = canvas.ToPpm();
            File.WriteAllText("../../../Chapter2Demo.ppm", ppm);
        }
    }
}
