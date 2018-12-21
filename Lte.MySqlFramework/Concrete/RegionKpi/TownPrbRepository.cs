using Abp.EntityFramework;
using Abp.EntityFramework.Entities.RegionKpi;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.RegionKpi;

namespace Lte.MySqlFramework.Concrete.RegionKpi
{
    public class TownPrbRepository : EfRepositorySave<MySqlContext, TownPrbStat>, ITownPrbRepository
    {
        public TownPrbRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public TownPrbStat Match(TownPrbStat stat)
        {
            return FirstOrDefault(x =>
                x.TownId == stat.TownId && x.FrequencyBandType == stat.FrequencyBandType &&
                x.StatTime == stat.StatTime);
        }
    }
}