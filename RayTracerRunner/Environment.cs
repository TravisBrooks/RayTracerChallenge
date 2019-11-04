using System;
using RayTracerChallenge;

namespace RayTracerRunner
{
    public class Environment
    {
        public Environment(Tuple3D gravity, Tuple3D wind)
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

        public Tuple3D Gravity { get; }
        public Tuple3D Wind { get; }
    }
}
