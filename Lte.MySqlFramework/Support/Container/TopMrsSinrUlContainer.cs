using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.Mr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.MySqlFramework.Support.Container
{
    public class TopMrsSinrUlContainer : ITopKpiContainer<TopMrsSinrUl>
    {
        public TopMrsSinrUl TopStat { get; set; }

        public int TopDates { get; set; }
    }
}
