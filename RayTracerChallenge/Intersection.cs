namespace RayTracerChallenge
{
    public class Intersection
    {
        public Intersection(double time, object theObject)
        {
            Time = time;
            TheObject = theObject;
        }

        public object TheObject { get; }
        public double Time { get; }
    }
}
