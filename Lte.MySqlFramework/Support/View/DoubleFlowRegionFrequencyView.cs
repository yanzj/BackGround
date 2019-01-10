using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.Dependency;
using Lte.MySqlFramework.Entities.RegionKpi;

namespace Lte.MySqlFramework.Support.View
{
    public class DoubleFlowRegionFrequencyView : IRegionFrequencyDateView<FrequencyDoubleFlowView>
    {
        public DateTime StatDate { get; set; }

        public string Region { get; set; }

        public IEnumerable<FrequencyDoubleFlowView> FrequencyViews { get; set; }
    }
}
