using System;
using System.Collections.Generic;
using Abp.EntityFramework.Dependency;
using Lte.MySqlFramework.Entities;

namespace Lte.MySqlFramework.Support.View
{
    public class PrbRegionDateView : IRegionDateSpanView<DistrictPrbView, TownPrbView>
    {
        public DateTime StatDate { get; set; }

        public IEnumerable<DistrictPrbView> DistrictViews { get; set; }

        public IEnumerable<TownPrbView> TownViews { get; set; }
    }
}
