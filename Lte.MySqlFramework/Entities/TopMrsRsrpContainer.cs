using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Entities.Mr;

namespace Lte.MySqlFramework.Entities
{
    public class TopMrsRsrpContainer : ITopKpiContainer<TopMrsRsrp>
    {
        public TopMrsRsrp TopStat { get; set; }

        public int TopDates { get; set; }
    }
}