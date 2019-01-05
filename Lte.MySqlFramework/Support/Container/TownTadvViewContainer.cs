using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.Entities.Mr;

namespace Lte.MySqlFramework.Support.Container
{
    public class TownTadvViewContainer
    {
        public IEnumerable<TownMrsTadv> MrsTadvs { get; set; }
        
        public IEnumerable<TownMrsTadv> CollegeMrsTadvs { get; set; }
        
        public IEnumerable<TownMrsTadv> MrsTadvs800 { get; set; }
        
        public IEnumerable<TownMrsTadv> MrsTadvs1800 { get; set; }
        
        public IEnumerable<TownMrsTadv> MrsTadvs2100 { get; set; }
    }
}
