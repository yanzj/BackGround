using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.RegionKpi;

namespace Lte.MySqlFramework.Support.View
{
    public class MrsTadvRegionFrequencyView : IRegionFrequencyView<FrequencyMrsTadvView>
    {
        public DateTime StatDate { get; set; }

        public string Region { get; set; }

        public IEnumerable<FrequencyMrsTadvView> FrequencyViews { get; set; }
    }
}
