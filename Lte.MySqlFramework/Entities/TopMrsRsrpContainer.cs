using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities;

namespace Lte.MySqlFramework.Entities
{
    public class TopMrsRsrpContainer : ITopKpiContainer<TopMrsRsrp>
    {
        public TopMrsRsrp TopStat { get; set; }

        public int TopDates { get; set; }
    }
}