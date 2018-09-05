using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.Dependency;
using Lte.MySqlFramework.Entities.RegionKpi;

namespace Lte.MySqlFramework.Support.View
{
    public class HourCqiRegionDateView : IRegionDateSpanView<DistrictHourCqiView, TownHourCqiView>
    {
        public DateTime StatDate { get; set; }

        public IEnumerable<DistrictHourCqiView> DistrictViews { get; set; }

        public IEnumerable<TownHourCqiView> TownViews { get; set; }
    }
}
