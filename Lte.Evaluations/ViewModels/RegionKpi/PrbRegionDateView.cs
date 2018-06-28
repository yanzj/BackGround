using Lte.MySqlFramework.Entities;
using System;
using System.Collections.Generic;
using Abp.EntityFramework.Dependency;

namespace Lte.Evaluations.ViewModels.RegionKpi
{
    public class PrbRegionDateView : IRegionDateSpanView<DistrictPrbView, TownPrbView>
    {
        public DateTime StatDate { get; set; }

        public IEnumerable<DistrictPrbView> DistrictViews { get; set; }

        public IEnumerable<TownPrbView> TownViews { get; set; }
    }
}
