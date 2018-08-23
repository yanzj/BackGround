using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.Dependency;
using Lte.MySqlFramework.Entities.RegionKpi;

namespace Lte.MySqlFramework.Support.View
{
    public class HourUsersRegionDateView : IRegionDateSpanView<DistrictHourUsersView, TownHourUsersView>
    {
        public DateTime StatDate { get; set; }

        public IEnumerable<DistrictHourUsersView> DistrictViews { get; set; }

        public IEnumerable<TownHourUsersView> TownViews { get; set; }
    }
}
