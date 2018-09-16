using System;
using System.Collections.Generic;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.RegionKpi;

namespace Lte.MySqlFramework.Support.View
{
    public class PreciseRegionDateView : IRegionDateSpanView<DistrictPreciseView, TownPreciseView>
    {
        public DateTime StatDate { get; set; }

        public IEnumerable<DistrictPreciseView> DistrictViews { get; set; } 

        public IEnumerable<TownPreciseView> TownViews { get; set; }
    }
}
