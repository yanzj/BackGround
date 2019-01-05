using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.Entities.Mr;

namespace Lte.MySqlFramework.Support.Container
{
    public class TownRsrpViewContainer
    {
        public IEnumerable<TownMrsRsrp> MrsRsrps { get; set; }

        public IEnumerable<TownMrsRsrp> CollegeMrsRsrps { get; set; }

        public IEnumerable<TownMrsRsrp> MrsRsrps800 { get; set; }
        
        public IEnumerable<TownMrsRsrp> MrsRsrps1800 { get; set; }

        public IEnumerable<TownMrsRsrp> MrsRsrps2100 { get; set; }

    }
}
