using System;
using System.Collections.Generic;

namespace Abp.EntityFramework.Entities.Cdma
{
    public class CdmaRegionDateView
    {
        public DateTime StatDate { get; set; }

        public IEnumerable<CdmaRegionStatView> StatViews { get; set; }
    }
}