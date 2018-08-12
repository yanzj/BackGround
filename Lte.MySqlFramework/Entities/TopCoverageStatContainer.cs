using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Test;

namespace Lte.MySqlFramework.Entities
{
    public class TopCoverageStatContainer : ITopKpiContainer<CoverageStat>
    {
        public CoverageStat TopStat { get; set; }

        public int TopDates { get; set; }
    }
}