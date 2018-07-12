using System.Collections.Generic;

namespace Lte.Domain.Common.Geo
{
    public interface IGeoPointList
    {
        List<GeoPoint> BoundaryGeoPoints { get; set; }
    }
}