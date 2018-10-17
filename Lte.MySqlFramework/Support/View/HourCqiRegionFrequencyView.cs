using Abp.EntityFramework.Dependency;
using Lte.MySqlFramework.Entities.RegionKpi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.MySqlFramework.Support.View
{
    public class HourCqiRegionFrequencyView : IRegionFrequencyDateView<FrequencyHourCqiView>
    {
        public DateTime StatDate { get; set; }

        public string Region { get; set; }

        public IEnumerable<FrequencyHourCqiView> FrequencyViews { get; set; }
    }
}
