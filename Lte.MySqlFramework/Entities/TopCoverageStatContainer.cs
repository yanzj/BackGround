using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities;

namespace Lte.MySqlFramework.Entities
{
    public class TopCoverageStatContainer : ITopKpiContainer<CoverageStat>
    {
        public CoverageStat TopStat { get; set; }

        public int TopDates { get; set; }
    }
}