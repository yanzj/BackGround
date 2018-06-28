using System.Collections.Generic;
using Lte.Domain.Common.Geo;
using Lte.Domain.Regular.Attributes;

namespace Lte.MySqlFramework.Entities
{
    [TypeDoc("镇区边界视图")]
    public class TownBoundariesView
    {
        [MemberDoc("镇的名称")]
        public string Town { get; set; }

        [MemberDoc("边界经纬度序列")]
        public IEnumerable<List<GeoPoint>> BoundaryGeoPoints { get; set; }
    }
}