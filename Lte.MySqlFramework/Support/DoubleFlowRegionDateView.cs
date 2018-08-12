using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.MySqlFramework.Entities;

namespace Lte.MySqlFramework.Support
{
    public class DoubleFlowRegionDateView : IRegionDateSpanView<DistrictDoubleFlowView, TownDoubleFlowView>
    {
        public DateTime StatDate { get; set; }

        public IEnumerable<DistrictDoubleFlowView> DistrictViews { get; set; }

        public IEnumerable<TownDoubleFlowView> TownViews { get; set; }
    }
}
