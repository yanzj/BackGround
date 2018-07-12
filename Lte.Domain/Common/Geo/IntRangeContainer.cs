using System.Collections.Generic;
using System.Linq;

namespace Lte.Domain.Common.Geo
{
    public class IntRangeContainer
    {
        public int West { get; set; }

        public int East { get; set; }

        public int South { get; set; }

        public int North { get; set; }

        public IntRangeContainer(IEnumerable<List<GeoPoint>> boundaries)
        {
            West = (int)((boundaries.Select(x => x.Select(t => t.Longtitute).Min()).Min() - 112) / 0.00049);
            East = (int)((boundaries.Select(x => x.Select(t => t.Longtitute).Max()).Max() - 112) / 0.00049);
            South = (int)((boundaries.Select(x => x.Select(t => t.Lattitute).Min()).Min() - 22) / 0.00045);
            North = (int)((boundaries.Select(x => x.Select(t => t.Lattitute).Max()).Max() - 22) / 0.00045);
        }
    }
}