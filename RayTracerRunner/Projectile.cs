using System;
using Tuple = RayTracerChallenge.Tuple;

namespace RayTracerRunner
{
    public class Projectile
    {
        public Projectile(Tuple position, Tuple velocity)
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

        public Tuple Position { get; }
        public Tuple Velocity { get; }
    }
}
