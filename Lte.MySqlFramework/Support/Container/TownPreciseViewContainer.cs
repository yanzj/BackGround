using System.Collections.Generic;
using Abp.EntityFramework.Entities.Mr;
using Abp.EntityFramework.Entities.RegionKpi;

namespace Lte.MySqlFramework.Support.Container
{
    public class TownPreciseViewContainer
    {
        public IEnumerable<TownPreciseView> Views { get; set; }

        public IEnumerable<TownMrsRsrp> MrsRsrps { get; set; }

        public IEnumerable<TownMrsSinrUl> MrsSinrUls { get; set; }
    }
}