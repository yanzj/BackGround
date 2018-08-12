using System.Collections.Generic;
using Abp.EntityFramework.Entities.Test;

namespace Lte.MySqlFramework.Entities
{
    public class TownAreaTestFileView
    {
        public string District { get; set; }

        public string Town { get; set; }

        public double Distance { get; set; }

        public double DistanceInMeter => Distance * 1000;

        public int Count { get; set; }

        public int CoverageCount { get; set; }

        public double CoverageRate => Count == 0 ? 0 : 100 * (double)CoverageCount / Count;

        public IEnumerable<AreaTestFileView> Views { get; set; }

    }
}