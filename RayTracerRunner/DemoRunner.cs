using System;
using System.Diagnostics;

namespace RayTracerRunner
{
    public abstract class DemoRunner
    {
        protected abstract void RunImpl();

        public void Run()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            RunImpl();

            stopwatch.Stop();
            var name = this.GetType().Name;
            Console.WriteLine($"{name} Elapsed seconds: {stopwatch.Elapsed.TotalSeconds}");
        }
    }
}
