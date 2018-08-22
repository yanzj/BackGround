using System;
using System.Collections.Generic;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.MySqlFramework.Entities;
using Lte.MySqlFramework.Entities.RegionKpi;

namespace Lte.MySqlFramework.Support.View
{
    public class DoubleFlowRegionDateView : IRegionDateSpanView<DistrictDoubleFlowView, TownDoubleFlowView>
    {
        public DateTime StatDate { get; set; }

        public IEnumerable<DistrictDoubleFlowView> DistrictViews { get; set; }

        public IEnumerable<TownDoubleFlowView> TownViews { get; set; }
    }
}
