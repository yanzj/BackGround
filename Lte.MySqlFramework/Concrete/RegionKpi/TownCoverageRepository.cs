using Abp.EntityFramework;
using Abp.EntityFramework.Entities.RegionKpi;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.RegionKpi;

namespace Lte.MySqlFramework.Concrete.RegionKpi
{
    public class TownCoverageRepository : EfRepositorySave<MySqlContext, TownCoverageStat>, ITownCoverageRepository
    {
        public TownCoverageRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public TownCoverageStat Match(TownCoverageStat stat)
        {
            return FirstOrDefault(x =>
                x.StatDate == stat.StatDate && x.TownId == stat.TownId &&
                x.FrequencyBandType == stat.FrequencyBandType);
        }
    }
}