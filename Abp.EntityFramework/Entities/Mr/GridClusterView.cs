using System.Collections.Generic;
using Lte.Domain.Common.Geo;

namespace Abp.EntityFramework.Entities.Mr
{
    public class GridClusterView
    {
        public string Theme { get; set; }

        public int ClusterNumber { get; set; }

        public IEnumerable<GeoGridPoint> GridPoints { get; set; }
    }
}