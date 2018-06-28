using System;
using System.Collections.Generic;

namespace Lte.MySqlFramework.Entities
{
    public class CdmaRegionDateView
    {
        public DateTime StatDate { get; set; }

        public IEnumerable<CdmaRegionStatView> StatViews { get; set; }
    }
}