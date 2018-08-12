using System;
using System.Collections.Generic;
using Abp.EntityFramework.Dependency;
using Lte.MySqlFramework.Entities;

namespace Lte.MySqlFramework.Support.View
{
    public class RrcRegionDateView : IRegionDateSpanView<DistrictRrcView, TownRrcView>
    {
        public DateTime StatDate { get; set; }

        public IEnumerable<DistrictRrcView> DistrictViews { get; set; }

        public IEnumerable<TownRrcView> TownViews { get; set; }
    }
}
