using Abp.EntityFramework;
using Abp.EntityFramework.Entities.RegionKpi;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.RegionKpi;

namespace Lte.MySqlFramework.Concrete.RegionKpi
{
    public class TownPreciseCoverageRepository : EfRepositorySave<MySqlContext, TownPreciseStat>,
        ITownPreciseCoverageRepository
    {
        public TownPreciseCoverageRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public TownPreciseStat Match(TownPreciseStat stat)
        {
            var endTime = stat.StatTime.AddDays(1);
            return FirstOrDefault(
                x => x.TownId == stat.TownId && x.StatTime >= stat.StatTime && x.StatTime < endTime
                     && x.FrequencyBandType == stat.FrequencyBandType);
        }
    }
}