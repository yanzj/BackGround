using Lte.MySqlFramework.Entities;
using System;
using System.Collections.Generic;
using Abp.EntityFramework.Dependency;

namespace Lte.Evaluations.ViewModels.RegionKpi
{
    public class MrsRegionDateView : IRegionDateSpanView<DistrictMrsRsrpView, TownMrsRsrpView>
    {
        public DateTime StatDate { get; set; }

        public IEnumerable<DistrictMrsRsrpView> DistrictViews { get; set; }

        public IEnumerable<TownMrsRsrpView> TownViews { get; set; }
    }
}
