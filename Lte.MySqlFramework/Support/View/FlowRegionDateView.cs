using System;
using System.Collections.Generic;
using Abp.EntityFramework.Dependency;
using Lte.MySqlFramework.Entities;

namespace Lte.MySqlFramework.Support.View
{
    public class FlowRegionDateView : IRegionDateSpanView<DistrictFlowView, TownFlowView>
    {
        public DateTime StatDate { get; set; }

        public IEnumerable<DistrictFlowView> DistrictViews { get; set; }

        public IEnumerable<TownFlowView> TownViews { get; set; }
    }
}
