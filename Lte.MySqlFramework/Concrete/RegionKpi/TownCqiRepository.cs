using Abp.EntityFramework;
using Abp.EntityFramework.Entities.RegionKpi;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.RegionKpi;

namespace Lte.MySqlFramework.Concrete.RegionKpi
{
    public class TownCqiRepository : EfRepositorySave<MySqlContext, TownCqiStat>, ITownCqiRepository
    {
        public TownCqiRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public TownCqiStat Match(TownCqiStat stat)
        {
            return FirstOrDefault(x =>
                x.TownId == stat.TownId && x.FrequencyBandType == stat.FrequencyBandType &&
                x.StatTime == stat.StatTime);
        }
    }
}