using Lte.MySqlFramework.Entities;
using System;
using System.Collections.Generic;
using Abp.EntityFramework.Dependency;

namespace Lte.Evaluations.ViewModels.RegionKpi
{
    public class FlowRegionDateView : IRegionDateSpanView<DistrictFlowView, TownFlowView>
    {
        public DateTime StatDate { get; set; }

        public IEnumerable<DistrictFlowView> DistrictViews { get; set; }

        public IEnumerable<TownFlowView> TownViews { get; set; }
    }
}
