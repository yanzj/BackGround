using System;
using System.Collections.Generic;
using Abp.EntityFramework.Dependency;
using Lte.MySqlFramework.Entities;

namespace Lte.MySqlFramework.Support.View
{
    public class CoverageRegionDateView : IRegionDateSpanView<DistrictCoverageView, TownCoverageView>
    {
        public DateTime StatDate { get; set; }

        public IEnumerable<DistrictCoverageView> DistrictViews { get; set; }

        public IEnumerable<TownCoverageView> TownViews { get; set; }
    }
}
