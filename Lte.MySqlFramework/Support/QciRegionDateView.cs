using System;
using System.Collections.Generic;
using Abp.EntityFramework.Dependency;
using Lte.MySqlFramework.Entities;

namespace Lte.MySqlFramework.Support
{
    public class QciRegionDateView : IRegionDateSpanView<DistrictQciView, TownQciView>
    {
        public DateTime StatDate { get; set; }

        public IEnumerable<DistrictQciView> DistrictViews { get; set; }

        public IEnumerable<TownQciView> TownViews { get; set; }
    }
}
