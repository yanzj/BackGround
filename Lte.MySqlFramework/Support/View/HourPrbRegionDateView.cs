using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.Dependency;
using Lte.MySqlFramework.Entities.RegionKpi;

namespace Lte.MySqlFramework.Support.View
{
    public class HourPrbRegionDateView : IRegionDateSpanView<DistrictHourPrbView, TownHourPrbView>
    {
        public DateTime StatDate { get; set; }

        public IEnumerable<DistrictHourPrbView> DistrictViews { get; set; }

        public IEnumerable<TownHourPrbView> TownViews { get; set; }
    }
}
