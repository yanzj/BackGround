using Lte.MySqlFramework.Entities;
using System;
using System.Collections.Generic;
using Abp.EntityFramework.Dependency;

namespace Lte.Evaluations.ViewModels.RegionKpi
{
    public class QciRegionDateView : IRegionDateSpanView<DistrictQciView, TownQciView>
    {
        public DateTime StatDate { get; set; }

        public IEnumerable<DistrictQciView> DistrictViews { get; set; }

        public IEnumerable<TownQciView> TownViews { get; set; }
    }

    public class CqiRegionDateView : IRegionDateSpanView<DistrictCqiView, TownCqiView>
    {
        public DateTime StatDate { get; set; }

        public IEnumerable<DistrictCqiView> DistrictViews { get; set; }

        public IEnumerable<TownCqiView> TownViews { get; set; }
    }

}
