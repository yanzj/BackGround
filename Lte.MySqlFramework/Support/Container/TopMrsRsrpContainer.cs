using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.Mr;

namespace Lte.MySqlFramework.Support.Container
{
    public class TopMrsRsrpContainer : ITopKpiContainer<TopMrsRsrp>
    {
        public TopMrsRsrp TopStat { get; set; }

        public int TopDates { get; set; }
    }
}