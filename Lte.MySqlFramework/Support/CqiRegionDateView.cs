using System;
using System.Collections.Generic;
using Abp.EntityFramework.Dependency;
using Lte.MySqlFramework.Entities;

namespace Lte.MySqlFramework.Support
{
    public class CqiRegionDateView : IRegionDateSpanView<DistrictCqiView, TownCqiView>
    {
        public DateTime StatDate { get; set; }

        public IEnumerable<DistrictCqiView> DistrictViews { get; set; }

        public IEnumerable<TownCqiView> TownViews { get; set; }
    }
}