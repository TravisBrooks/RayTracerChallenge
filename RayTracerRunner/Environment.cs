using System;
using Tuple = RayTracerChallenge.Tuple;

namespace RayTracerRunner
{
    public class Environment
    {
        public Environment(Tuple gravity, Tuple wind)
        {
            if (gravity.IsVector == false)
            {
                throw new ArgumentException("The gravity must be a vector");
            }
            if (wind.IsVector == false)
            {
                throw new ArgumentException("The wind must be a vector");
            }
            Gravity = gravity;
            Wind = wind;
        }

        public Tuple Gravity { get; }
        public Tuple Wind { get; }
    }
}
