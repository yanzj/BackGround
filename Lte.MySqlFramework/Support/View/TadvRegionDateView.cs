using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.Dependency;
using Lte.MySqlFramework.Entities.Mr;

namespace Lte.MySqlFramework.Support.View
{
    public class TadvRegionDateView : IRegionDateSpanView<DistrictMrsTadvView, TownMrsTadvView>
    {
        public DateTime StatDate { get; set; }

        public IEnumerable<DistrictMrsTadvView> DistrictViews { get; set; }

        public IEnumerable<TownMrsTadvView> TownViews { get; set; }
    }
}
