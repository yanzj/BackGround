using Lte.MySqlFramework.Entities;
using System;
using System.Collections.Generic;
using Abp.EntityFramework.Dependency;

namespace Lte.Evaluations.ViewModels.RegionKpi
{
    public class RrcRegionDateView : IRegionDateSpanView<DistrictRrcView, TownRrcView>
    {
        public DateTime StatDate { get; set; }

        public IEnumerable<DistrictRrcView> DistrictViews { get; set; }

        public IEnumerable<TownRrcView> TownViews { get; set; }
    }
}
