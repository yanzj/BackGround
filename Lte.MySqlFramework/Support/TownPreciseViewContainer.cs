using System.Collections.Generic;
using Abp.EntityFramework.Entities;

namespace Lte.MySqlFramework.Support
{
    public class TownPreciseViewContainer
    {
        public IEnumerable<TownPreciseView> Views { get; set; }

        public IEnumerable<TownMrsRsrp> MrsRsrps { get; set; }

        public IEnumerable<TownMrsSinrUl> MrsSinrUls { get; set; }
    }
}