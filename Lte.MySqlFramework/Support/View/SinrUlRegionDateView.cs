using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.Dependency;
using Lte.MySqlFramework.Entities.Mr;

namespace Lte.MySqlFramework.Support.View
{
    public class SinrUlRegionDateView : IRegionDateSpanView<DistrictMrsSinrUlView, TownMrsSinrUlView>
    {
        public DateTime StatDate { get; set; }

        public IEnumerable<DistrictMrsSinrUlView> DistrictViews { get; set; }

        public IEnumerable<TownMrsSinrUlView> TownViews { get; set; }
    }
}
