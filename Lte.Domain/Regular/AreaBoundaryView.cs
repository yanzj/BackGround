using System.Collections.Generic;
using Lte.Domain.Common.Geo;

namespace Lte.Domain.Regular
{
    public class AreaBoundaryView : IGeoPointList
    {
        public string AreaName { get; set; }

        public string AreaType { get; set; }

        public List<GeoPoint> BoundaryGeoPoints { get; set; }
    }
}