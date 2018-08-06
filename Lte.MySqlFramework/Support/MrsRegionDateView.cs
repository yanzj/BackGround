using System;
using System.Collections.Generic;
using Abp.EntityFramework.Dependency;
using Lte.MySqlFramework.Entities;

namespace Lte.MySqlFramework.Support
{
    public class MrsRegionDateView : IRegionDateSpanView<DistrictMrsRsrpView, TownMrsRsrpView>
    {
        public DateTime StatDate { get; set; }

        public IEnumerable<DistrictMrsRsrpView> DistrictViews { get; set; }

        public IEnumerable<TownMrsRsrpView> TownViews { get; set; }
    }
}
