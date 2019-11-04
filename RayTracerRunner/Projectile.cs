using System;
using RayTracerChallenge;

namespace RayTracerRunner
{
    public class Projectile
    {
        public Projectile(Tuple3D position, Tuple3D velocity)
        {
            if (position.IsPoint == false)
            {
                throw new ArgumentException("the position tuple must be a point");
            }

            if (velocity.IsVector == false)
            {
                throw new ArgumentException("The velocity must be a vector");
            }

            Position = position;
            Velocity = velocity;
        }

        public Tuple3D Position { get; }
        public Tuple3D Velocity { get; }
    }
}
