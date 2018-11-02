using System.Collections.Generic;
using Abp.EntityFramework.Entities.Mr;
using Abp.EntityFramework.Entities.RegionKpi;

namespace Lte.MySqlFramework.Support.Container
{
    public class TownPreciseViewContainer
    {
        public IEnumerable<TownPreciseView> Views { get; set; }
        
        public IEnumerable<TownPreciseStat> CollegeStats { get; set; }

        public IEnumerable<TownPreciseView> Views800 { get; set; }

        public IEnumerable<TownPreciseView> Views1800 { get; set; }

        public IEnumerable<TownPreciseView> Views2100 { get; set; }

        public IEnumerable<TownMrsRsrp> MrsRsrps { get; set; }

        public IEnumerable<TownMrsRsrp> CollegeMrsRsrps { get; set; }

        public IEnumerable<TownMrsRsrp> MrsRsrps800 { get; set; }
        
        public IEnumerable<TownMrsRsrp> MrsRsrps1800 { get; set; }

        public IEnumerable<TownMrsRsrp> MrsRsrps2100 { get; set; }

    }
}