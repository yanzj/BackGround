using System.Collections.Generic;

namespace Abp.EntityFramework.Entities.Cdma
{
    public class CdmaRegionStatTrend
    {
        public IEnumerable<string> StatDates { get; set; }

        public List<string> RegionList { get; set; }

        public List<IEnumerable<CdmaRegionStatView>> ViewList { get; set; }
    }
}