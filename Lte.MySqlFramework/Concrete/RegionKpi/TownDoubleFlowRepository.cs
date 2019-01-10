using Abp.EntityFramework;
using Abp.EntityFramework.Entities.RegionKpi;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.RegionKpi;

namespace Lte.MySqlFramework.Concrete.RegionKpi
{
    public class TownDoubleFlowRepository : EfRepositorySave<MySqlContext, TownDoubleFlow>, ITownDoubleFlowRepository
    {
        public TownDoubleFlowRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public TownDoubleFlow Match(TownDoubleFlow stat)
        {
            return FirstOrDefault(x =>
                x.TownId == stat.TownId && x.FrequencyBandType == stat.FrequencyBandType &&
                x.StatTime == stat.StatTime);
        }
    }
}