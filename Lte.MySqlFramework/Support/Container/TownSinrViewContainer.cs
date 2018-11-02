using Abp.EntityFramework.Entities.Mr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.MySqlFramework.Support.Container
{
    public class TownSinrViewContainer
    {
        public IEnumerable<TownMrsSinrUl> MrsSinrUls { get; set; }
    }
}
