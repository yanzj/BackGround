using System;
using System.Collections.Generic;
using Abp.EntityFramework.Dependency;
using Lte.Parameters.Entities.Kpi;

namespace Lte.Evaluations.ViewModels.RegionKpi
{
    public class PreciseRegionDateView : IRegionDateSpanView<DistrictPreciseView, TownPreciseView>
    {
        public DateTime StatDate { get; set; }

        public IEnumerable<DistrictPreciseView> DistrictViews { get; set; } 

        public IEnumerable<TownPreciseView> TownViews { get; set; }
    }
}
