using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RayTracerChallenge
{
    public class IntersectionList : IReadOnlyList<Intersection>
    {
        private readonly List<Intersection> _intersections;

        public IntersectionList(params Intersection[] intersections)
        {
            _intersections = new List<Intersection>(intersections);
        }

        public IntersectionList(IEnumerable<Intersection> intersections)
        {
            _intersections = new List<Intersection>(intersections);
        }

        private bool _foundHit;
        private Intersection _hit;
        public Intersection Hit()
        {
            if (_foundHit == false)
            {
                _hit = _intersections.Where(i => i.Time >= 0).OrderBy(i => i.Time).FirstOrDefault();
                _foundHit = true;
            }

            return _hit;
        }

        public IEnumerator<Intersection> GetEnumerator()
        {
            return _intersections.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => _intersections.Count;

        public Intersection this[int index] => _intersections[index];
    }
}
