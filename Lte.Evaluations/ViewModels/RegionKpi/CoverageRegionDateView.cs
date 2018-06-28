using Lte.MySqlFramework.Entities;
using System;
using System.Collections.Generic;
using Abp.EntityFramework.Dependency;

namespace Lte.Evaluations.ViewModels.RegionKpi
{
    public class CoverageRegionDateView : IRegionDateSpanView<DistrictCoverageView, TownCoverageView>
    {
        public DateTime StatDate { get; set; }

        public IEnumerable<DistrictCoverageView> DistrictViews { get; set; }

        public IEnumerable<TownCoverageView> TownViews { get; set; }
    }
}
